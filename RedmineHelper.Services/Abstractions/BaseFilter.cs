using System.Linq;

namespace RedmineHelper.Services.Abstractions
{
    public abstract class BaseFilter
    {
        public string Assigned_To_Id { get; set; } = "me";

        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 10;

        public override string ToString()
        {
            var data = GetType().GetProperties().Select(x => $"{x.Name}={x.GetValue(this)}");
            return data.Aggregate((x, y) => x.StartsWith("?") ? $"{x}&{y}" : $"?{x}&{y}").ToLower();
        }
    }
}
