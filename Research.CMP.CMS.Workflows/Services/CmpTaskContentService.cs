using System.Text.Json;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction.Activities;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Web;
using Microsoft.Extensions.Logging;
using Research.CMP.CMS.Workflows.Helpers;
using Research.CMP.CMS.Workflows.Models;
using Research.CMP.CMS.Workflows.REST.EWM;
using Research.CMP.CMS.Workflows.REST.Tasks;
using Research.CMP.CMS.Workflows.UI;

namespace Research.CMP.CMS.Workflows.Services;

public class CmpTaskContentService
{
    private readonly CmpApiClient _cmpApiClient;
    private readonly ILogger<CmpTaskContentService> _log;
    private readonly IContentRepository _contentRepository;
    private readonly IContentLoader _contentLoader;
    private readonly IActivityQueryService _activityQueryService;
    private readonly CmpTasksContentLocator _cmpTasksContentLocator;

    public CmpTaskContentService(
        IContentRepository contentRepository,
        IContentLoader contentLoader,
        IActivityRepository activityRepository,
        CmpTasksContentLocator cmpTasksContentLocator,
        CmpApiClient cmpApiClient,
        ILogger<CmpTaskContentService> logger)

    {
        _contentRepository = contentRepository;
        _contentLoader = contentLoader;
        _cmpTasksContentLocator = cmpTasksContentLocator;
        _cmpApiClient = cmpApiClient;
        _log = logger;
    }
    public void SyncContent(ExternalWorkManagement request)
    {
        if (request.Data.ExternalWork.ExternalSystem != CMPTasksConstants.ExternalSystemName)
        {
            return;
        }

        try
        {
            
            var cmpTasksFolderRef = GetOrCreateCmpTasksFolder();
            _log.LogDebug($"cmpTasksFolderRef = {cmpTasksFolderRef.ID}");

            var allCmpTasks = _cmpTasksContentLocator
                .GetAll<CmpTaskBlock>(SiteDefinition.Current.GlobalAssetsRoot);
            
            var isExistingItem = allCmpTasks
                .Any( t => 
                    t.TaskId == request.Data.Task.Id && 
                    t.StepId == request.Data.Step.Id &&
                    t.SubstepId == request.Data.SubStep.Id);
            var remoteTask = _cmpApiClient.GetData<CmpTaskRestModel>(request.Data.Task.Links.Self.ToString()).Result;
            var remoteSubStep = remoteTask.Steps
                .First(s => s.Id == request.Data.Step.Id)
                .SubSteps.First(ss => ss.Id == request.Data.SubStep.Id);
            if (isExistingItem)
            {
                var existingItem = allCmpTasks.First(t =>
                    t.TaskId == request.Data.Task.Id &&
                    t.StepId == request.Data.Step.Id &&
                    t.SubstepId == request.Data.SubStep.Id);
                existingItem = existingItem.CreateWritableClone() as CmpTaskBlock;
                if (existingItem != null)
                {
                    (existingItem as IContent).Name = remoteTask.Title;
                    existingItem.Comment = remoteTask.Steps.First(s => s.Id == request.Data.Step.Id).Description;
                    existingItem.Status = GetStepStatus(remoteTask, request.Data.Step.Id, request.Data.SubStep.Id);
                    existingItem.JsonData = JsonSerializer.Serialize(request);
                    _contentRepository.Save((IContent) existingItem, 
                        SaveAction.Publish | SaveAction.ForceCurrentVersion,
                        AccessLevel.NoAccess);
                }
            }
            else
            {
                var cmpTask = _contentRepository.GetDefault<CmpTaskBlock>(cmpTasksFolderRef);
                cmpTask.TaskId = request.Data.Task.Id;
                cmpTask.StepId = request.Data.Step.Id;
                cmpTask.SubstepId = request.Data.SubStep.Id;
                cmpTask.Status = GetStepStatus(remoteTask, request.Data.Step.Id, request.Data.SubStep.Id);
                cmpTask.JsonData = JsonSerializer.Serialize(request);
                (cmpTask as IContent).Name = remoteTask.Title;
                cmpTask.Comment = remoteTask.Steps.First(s => s.Id == request.Data.Step.Id).Description;
                _contentRepository.Save((IContent) cmpTask,
                    SaveAction.Publish | SaveAction.ForceCurrentVersion,
                    AccessLevel.NoAccess);
            }
        }
        catch (Exception e)
        {
            _log.LogError(e.Message);
        }
    }

    private ContentReference GetOrCreateCmpTasksFolder()
    {
        var cmpTasksFolder = _contentLoader
            .GetChildren<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot)
            .FirstOrDefault(f => f.Name == CMPTasksConstants.FolderName);
        ContentReference cmpTasksFolderRef = null;
        if(cmpTasksFolder == null)
        {
            cmpTasksFolder = _contentRepository.GetDefault<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot);
            cmpTasksFolder.Name = CMPTasksConstants.FolderName;
            cmpTasksFolderRef = _contentRepository.Save(cmpTasksFolder, AccessLevel.NoAccess);
        }
        else
        {
            cmpTasksFolderRef = cmpTasksFolder.ContentLink;
        }

        return cmpTasksFolderRef;
    }
    
    private string GetStepStatus(CmpTaskRestModel remoteTask, string stepId, string subStepId)
    {
        var subStep = remoteTask.Steps.First(s => s.Id == stepId).SubSteps.First(ss => ss.Id == subStepId);
        if (subStep.IsCompleted ?? false)
        {
           return StatusSelectionFactory.Completed;
        }
        if (subStep.IsInProgress ?? false)
        {
            return StatusSelectionFactory.InProgress;
        }
        return StatusSelectionFactory.NotStarted;
    }
}