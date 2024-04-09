namespace Research.CMP.CMS.Workflows.Models;

[Options(ConfigurationSection = ConfigurationSectionConstants.Cms)]
public class CmpCmsWorkflowOptions
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ExternalSystemId { get; set; }
}