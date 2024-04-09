using Microsoft.Extensions.DependencyInjection;
using Research.CMP.CMS.Workflows.Services;

namespace Research.CMP.CMS.Workflows;

public static class ServiceCollectionHelpers
{
    public static void AddCmpTasks(this IServiceCollection services)
    {
        services.AddSingleton<CmpTaskContentService>();
        services.AddSingleton(new CmpApiClient("9e48e6dd-971f-4e14-8b3e-3daafdeae241",
            "433c82b6027510c101b479ec121f7e9a86a904705c668bd0ad658e7b1505fcc2"));
    }

}