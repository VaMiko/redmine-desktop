using System.Threading.Tasks;
using RedmineHelper.Models.Dto;
using RedmineHelper.Services.Filters;

namespace RedmineHelper.Services.Abstractions
{
    public interface ITaskAdapter
    {
        public Task<RedmineTaskDto[]> GetTasks(TaskFilter taskFilter);

        public Task<bool> WriteOffHours(TimeEntry[] data);
    }
}
