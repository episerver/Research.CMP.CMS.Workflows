using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;

namespace Research.CMP.CMS.Workflows.UI;

[ServiceConfiguration(typeof(IContentQueryCategory), Lifecycle = ServiceInstanceScope.Singleton)]
public class CmpQueryCategory : IContentQueryCategory {
    public string DisplayName => "CMP Tasks";
    public int SortOrder => 100;
}