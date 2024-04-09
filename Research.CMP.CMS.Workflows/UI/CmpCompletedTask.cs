using EPiServer;
using EPiServer.Cms.Shell.UI.Rest.ContentQuery;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using EPiServer.Shell.Notification;
using EPiServer.Web;
using Research.CMP.CMS.Workflows.Models;

namespace Research.CMP.CMS.Workflows.UI;

[ServiceConfiguration(typeof (IContentQuery))]
public class CmpCompletedTask: ContentQueryBase, IContentQuery<CmpQueryCategory> {
    private readonly IContentRepository contentRepository;
    private readonly IInUseNotificationRepository inUseNotificationRepository;
    private readonly IContentLoader contentLoader;

    public CmpCompletedTask(IContentQueryHelper queryHelper, IContentRepository contentRepository, IContentLoader contentLoader,
        IInUseNotificationRepository inUseNotificationRepository): base(contentRepository, queryHelper) {
        this.contentRepository = contentRepository;
        this.contentLoader = contentLoader;
        this.inUseNotificationRepository = inUseNotificationRepository;
    }
    public override string Name => "completedTask";
    public override string DisplayName => "Completed";
    public override IEnumerable < string > PlugInAreas => new string[] {
        KnownContentQueryPlugInArea.EditorTasks
    };
    public override int SortOrder => 105;
    public override bool VersionSpecific => false;
    

    protected override IEnumerable <IContent> GetContent(ContentQueryParameters parameters)
    {
        return contentLoader
            .GetDescendents(SiteDefinition.Current.GlobalAssetsRoot)
            .Select(i => contentLoader.Get<IContent>(i))
            .Where(i => i is CmpTaskBlock && (i as CmpTaskBlock).Status == StatusSelectionFactory.Completed);
    }
}