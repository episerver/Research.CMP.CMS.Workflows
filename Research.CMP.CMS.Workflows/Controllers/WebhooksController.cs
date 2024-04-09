using System.Collections.ObjectModel;
using EPiServer;
using EPiServer.Cms.Shell.UI.Rest.Activities.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction.Activities;
using Research.CMP.CMS.Workflows.REST.EWM;
using Research.CMP.CMS.Workflows.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Research.CMP.CMS.Workflows.Controllers;


[Route("api/[controller]")]
[ApiController]
public class WebhooksController : ControllerBase
{
    private readonly IContentRepository _contentRepository;
    private readonly ActivityService _activityService;
    private readonly CmpTaskContentService _cmpTaskContentService;

    public WebhooksController(
        IContentRepository contentRepository,
        ActivityService activityService,
        CmpTaskContentService cmpTaskContentService)
    {
        _contentRepository = contentRepository;
        _activityService = activityService;
        _cmpTaskContentService = cmpTaskContentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new {message = "Hello World"});
    }
    
    [HttpGet("comment/{id}")]
    public async Task<IActionResult> GetComment(string id)
    {
        var data = await
            _activityService
                .ListActivitiesAsync(new Collection<ContentReference>
                {
                    new(id)
                });
        var comments = data.Activities.Where(a => a is ContentMessageActivity);
        return Ok(
            new
            {
                comments
            });
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ExternalWorkManagement request)
    {
        _cmpTaskContentService.SyncContent(request);
        Console.WriteLine(JsonConvert.SerializeObject(request));
        return Ok("OK");
    }

}
