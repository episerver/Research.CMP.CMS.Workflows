using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Research.CMP.CMS.Workflows.UI;

namespace Research.CMP.CMS.Workflows.Models;

[ContentType(DisplayName = "Cmp Task", GUID = "04A8F457-E41F-4CB1-9C12-6374FA1C58D6", AvailableInEditMode = true, GroupName = "CMP")]
public class CmpTaskBlock : BlockData
{
    [Display(Order = 1, GroupName = SystemTabNames.Content)]
    [Editable(false)]
    public virtual string Comment { get; set; }

    [Display(Order = 1, GroupName = SystemTabNames.Content)]
    [SelectOne(SelectionFactoryType = typeof(StatusSelectionFactory))]
    public virtual string Status { get; set; } = StatusSelectionFactory.NotStarted;
    
    [Display(Order = 10, GroupName = CMPTasksConstants.TabName)]
    [Editable(false)]
    public virtual string TaskId { get; set; }
    [Display(Order = 10, GroupName = CMPTasksConstants.TabName)]
    [Editable(false)]
    public virtual string StepId { get; set; }
    [Display(Order = 10, GroupName = CMPTasksConstants.TabName)]
    [Editable(false)]
    public virtual string SubstepId { get; set; }
    [Display(Order = 10, GroupName = CMPTasksConstants.TabName)]
    [Editable(false)]
    public virtual string JsonData { get; set; }
}