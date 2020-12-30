using Newtonsoft.Json;

namespace RedmineHelper.Models.Dto
{
    using System;
    public class SpentTimeDto
    {
        [JsonProperty(PropertyName = "issue_id")]
        public long IssueId { get; set; }

        [JsonProperty(PropertyName = "spent_on")]
        public DateTime Spent_on { get; set; } = DateTime.Today;

        [JsonProperty(PropertyName = "hours")]
        public decimal Hours  { get; set; }

        [JsonProperty(PropertyName = "activity_id")]
        public Activity Activity_id { get; set; } = Activity.Development;

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; } = ".";
    }
}
