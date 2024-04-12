using Newtonsoft.Json;

namespace Research.CMP.CMS.Workflows.REST.EWM
{
    public partial class ExternalWorkManagement
    {
        [JsonProperty("event_name", NullValueHandling = NullValueHandling.Ignore)]
        public string EventName { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("task", NullValueHandling = NullValueHandling.Ignore)]
        public EwmTask Task { get; set; }

        [JsonProperty("step", NullValueHandling = NullValueHandling.Ignore)]
        public Step Step { get; set; }

        [JsonProperty("sub_step", NullValueHandling = NullValueHandling.Ignore)]
        public SubStep SubStep { get; set; }

        [JsonProperty("external_work", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalWork ExternalWork { get; set; }
    }

    public partial class ExternalWork
    {
        [JsonProperty("external_system", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalSystem { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalWorkLinks Links { get; set; }
    }

    public partial class ExternalWorkLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("task", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Task { get; set; }

        [JsonProperty("sub_step", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SubStep { get; set; }
    }

    public partial class Step
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public StepLinks Links { get; set; }
    }

    public partial class StepLinks
    {
        [JsonProperty("task", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Task { get; set; }
    }

    public partial class SubStep
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

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
    }

    public partial class EwmTask
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public EwmTaskLinks Links { get; set; }
    }

    public partial class EwmTaskLinks
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Assets { get; set; }
    }
}