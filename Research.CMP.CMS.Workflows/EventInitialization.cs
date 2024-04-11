using System.Text.Json;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using Flurl;
using Research.CMP.CMS.Workflows.Models;
using Research.CMP.CMS.Workflows.REST.EWM;
using Research.CMP.CMS.Workflows.UI;

namespace Research.CMP.CMS.Workflows;

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class EventInitialization : IInitializableModule
{
    private static bool _initialized;
    private static CmpApiClient _client;

    public void Initialize(InitializationEngine context)
    {
        if (_initialized)
        {
            return;
        }

        var contentEvents = context.Locate.ContentEvents();
        contentEvents.PublishingContent += OnPublishingContent;
        _client = ServiceLocator.Current.GetInstance<CmpApiClient>();

        _initialized = true;
    }

    private void OnPublishingContent(object? sender, ContentEventArgs e)
    {
        if (e.Content is CmpTaskBlock cmptask)
        {
            var url = CmpApiConstants.APIBaseUrl
                .AppendPathSegments(
                    CmpApiConstants.TasksPath, cmptask.TaskId,
                    CmpApiConstants.StepsPath, cmptask.StepId,
                    CmpApiConstants.SubStepsPath, cmptask.SubstepId); // create path /v3/tasks/tid/steps/sid/sub-steps/ss-id
            try
            {
                _client.PatchTask(url,  new
                {
                    is_completed = (cmptask.Status == StatusSelectionFactory.Completed)
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //TODO: handle task completed to task in-progress transition
            }
        }
    }

    public void Uninitialize(InitializationEngine context)
    {
        var contentEvents = context.Locate.ContentEvents();
        contentEvents.PublishingContent -= OnPublishingContent;
    }
}