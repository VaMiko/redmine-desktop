namespace RedmineHelper.Services.Implementations
{
    using System.Threading.Tasks;
    using Models.Dto;
    using Abstractions;
    using Filters;
    public class RedmineTaskAdapter : ITaskAdapter
    {
        private readonly RedmineHttpClient _client;

        public RedmineTaskAdapter(RedmineHttpClient client)
        {
            _client = client;
        }

        public Task<RedmineTaskDto[]> GetTasks(TaskFilter taskFilter) => _client.GetMyTasks(taskFilter);

        public Task<bool> WriteOffHours(TimeEntry[] data) => _client.WriteOffHours(data);
    }
}

