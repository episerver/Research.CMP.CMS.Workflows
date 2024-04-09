using System.ComponentModel.DataAnnotations;
using EPiServer.DataAnnotations;

namespace Research.CMP.CMS.Workflows.UI;

[GroupDefinitions]
public static class GroupNames {
    [Display(Order = 47)]
    public
        const string TaskInfo = CMPTasksConstants.TabName;
}