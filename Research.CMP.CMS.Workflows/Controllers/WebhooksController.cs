using System.Collections.ObjectModel;
using EPiServer;
using EPiServer.Cms.Shell.UI.Rest.Activities.Internal;
using Microsoft.Extensions.Options;
using EPiServer.DataAbstraction.Activities;
using Research.CMP.CMS.Workflows.REST.EWM;
using Research.CMP.CMS.Workflows.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Research.CMP.CMS.Workflows.Models;

namespace Research.CMP.CMS.Workflows.Controllers;


[Route("api/cmp-cms-workflows/webhooks")]
[ApiController]
public class WebhooksController : ControllerBase
{
    private readonly IContentRepository _contentRepository;
    private readonly ActivityService _activityService;
    private readonly CmpTaskContentService _cmpTaskContentService;
    private readonly IOptions<CmpCmsWorkflowOptions> _options;

    public WebhooksController(
        IContentRepository contentRepository,
        ActivityService activityService,
        CmpTaskContentService cmpTaskContentService,
        IOptions<CmpCmsWorkflowOptions> options)
    {
        _contentRepository = contentRepository;
        _activityService = activityService;
        _cmpTaskContentService = cmpTaskContentService;
        _options = options;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new {message = "system is up and running", external_system_id = _options.Value.ExternalSystemId});
    }
    
    //TODO: Implement Comments
    
    // [HttpGet("comment/{id}")]
    // public async Task<IActionResult> GetComment(string id)
    // {
    //     var data = await
    //         _activityService
    //             .ListActivitiesAsync(new Collection<ContentReference>
    //             {
    //                 new(id)
    //             });
    //     var comments = data.Activities.Where(a => a is ContentMessageActivity);
    //     return Ok(
    //         new
    //         {
    //             comments
    //         });
    // }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ExternalWorkManagement request)
    {
        if (request.Data.ExternalWork.ExternalSystem != _options.Value.ExternalSystemId)
            return Ok("ID not found");
        
        _cmpTaskContentService.SyncContent(request);
        Console.WriteLine(JsonConvert.SerializeObject(request));
        return Ok("OK");
    }
}
