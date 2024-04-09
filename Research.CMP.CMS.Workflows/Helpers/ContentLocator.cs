using System.Globalization;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using EPiServer.Web;

namespace Research.CMP.CMS.Workflows.Helpers;

[ServiceConfiguration(Lifecycle = ServiceInstanceScope.Singleton)]
public class CmpTasksContentLocator
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentProviderManager _providerManager;
    private readonly IPageCriteriaQueryService _pageCriteriaQueryService;
    private readonly IContentTypeRepository _contentTypeRepository;

    public CmpTasksContentLocator(IContentLoader contentLoader,
        IContentProviderManager providerManager,
        IPageCriteriaQueryService pageCriteriaQueryService,
        IContentTypeRepository contentTypeRepository)
    {
        _contentLoader = contentLoader;
        _providerManager = providerManager;
        _pageCriteriaQueryService = pageCriteriaQueryService;
        _contentTypeRepository = contentTypeRepository;
    }

    public virtual IEnumerable<T> GetAll<T>(ContentReference rootLink)
    {
        var children = _contentLoader.GetChildren<IContent>(rootLink);
        foreach (var child in children)
        {
            if (child is T childOfRequestedTyped)
            {
                yield return childOfRequestedTyped;
            }
            foreach (var descendant in GetAll<T>(child.ContentLink))
            {
                yield return descendant;
            }
        }
    }
    

    // Type specified through page type ID
    public IEnumerable<T> FindPagesByPageTypeRecursively<T>(PageReference? pageLink = null) where T: PageData
    {
        if(pageLink == null)
        {
            pageLink = SiteDefinition.Current.RootPage.ToPageReference();
        }
        var criteria = new PropertyCriteriaCollection
        {
            new()
            {
                Name = "PageTypeID",
                Type = PropertyDataType.PageType,
                Condition = CompareCondition.Equal,
                Value = _contentTypeRepository.Load<T>().ID.ToString(CultureInfo.InvariantCulture)
            }
        };

        return _pageCriteriaQueryService.FindPagesWithCriteria(pageLink, criteria).Cast<T>();
    }
}