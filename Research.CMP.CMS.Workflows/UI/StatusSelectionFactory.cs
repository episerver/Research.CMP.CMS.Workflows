using EPiServer.Shell.ObjectEditing;

namespace Research.CMP.CMS.Workflows.UI;

public class StatusSelectionFactory : ISelectionFactory
{
    public static string NotStarted = "not_started";
    public static string InProgress = "in_progress";
    public static string Completed = "completed";
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return new ISelectItem[] {
            new SelectItem
            {
                Text = "Not Started",
                Value = NotStarted, 
            },
            new SelectItem
            {
                Text = "In Progress",
                Value = InProgress 
            },
            new SelectItem
            {
                Text = "Completed",
                Value = Completed 
            }
        };
    }
}