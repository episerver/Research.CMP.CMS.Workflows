using Newtonsoft.Json;

namespace Research.CMP.CMS.Workflows.REST.Tasks
{
    public partial class CmpTaskRestModel
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("reference_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceId { get; set; }

        [JsonProperty("start_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartAt { get; set; }

        [JsonProperty("due_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DueAt { get; set; }

        [JsonProperty("is_completed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsCompleted { get; set; }

        [JsonProperty("is_archived", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsArchived { get; set; }

        [JsonProperty("labels", NullValueHandling = NullValueHandling.Ignore)]
        public Label[] Labels { get; set; }

        [JsonProperty("steps", NullValueHandling = NullValueHandling.Ignore)]
        public Step[] Steps { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public CmpTaskRestModelLinks Links { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }

    public partial class Label
    {
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public Group Group { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public Group[] Values { get; set; }
    }

    public partial class Group
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public partial class CmpTaskRestModelLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("campaign", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Campaign { get; set; }

        [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Assets { get; set; }

        [JsonProperty("custom_fields", NullValueHandling = NullValueHandling.Ignore)]
        public object CustomFields { get; set; }

        [JsonProperty("brief", NullValueHandling = NullValueHandling.Ignore)]
        public object Brief { get; set; }

        [JsonProperty("attachments", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Attachments { get; set; }

        [JsonProperty("web_urls", NullValueHandling = NullValueHandling.Ignore)]
        public WebUrls WebUrls { get; set; }
    }

    public partial class WebUrls
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("brief", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Brief { get; set; }
    }

    public partial class Step
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("is_completed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsCompleted { get; set; }

        [JsonProperty("due_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DueAt { get; set; }

        [JsonProperty("sub_steps", NullValueHandling = NullValueHandling.Ignore)]
        public SubStep[] SubSteps { get; set; }
    }

    public partial class SubStep
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("is_completed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsCompleted { get; set; }

        [JsonProperty("is_in_progress", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInProgress { get; set; }

        [JsonProperty("is_skipped", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSkipped { get; set; }

        [JsonProperty("is_external", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsExternal { get; set; }

        [JsonProperty("assignee_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AssigneeId { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public SubStepLinks Links { get; set; }
    }

    public partial class SubStepLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("task", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Task { get; set; }

        [JsonProperty("external_work", NullValueHandling = NullValueHandling.Ignore)]
        public Uri ExternalWork { get; set; }

        [JsonProperty("assignee", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Assignee { get; set; }
    }
}
