namespace RedmineHelper.Models.Dto
{
    using Newtonsoft.Json;
    public class TimeEntry
    {
        [JsonProperty(PropertyName = "time_entry")]
        public SpentTimeDto data { get; set; }
    }
}
