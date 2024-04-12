using Microsoft.Extensions.DependencyInjection;
using Research.CMP.CMS.Workflows.Models;
using Research.CMP.CMS.Workflows.Services;

namespace Research.CMP.CMS.Workflows;

public static class ServiceCollectionHelpers
{
    public static void AddCmpCmsWorkflows(this IServiceCollection services, Action<CmpCmsWorkflowOptions>? configureOptions = null)
    {
        if (configureOptions is not null)
        {
            services.Configure(configureOptions);
        }
        services.AddSingleton<CmpTaskContentService>();
        services.AddSingleton<CmpApiClient>();
    }
}