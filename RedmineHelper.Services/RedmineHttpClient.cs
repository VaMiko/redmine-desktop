using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedmineHelper.Models.Dto;
using RedmineHelper.Services.Filters;

namespace RedmineHelper.Services
{
    public class RedmineHttpClient
    {
        private readonly HttpClient _client;

        public RedmineHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<RedmineTaskDto[]> GetMyTasks(TaskFilter filter)
        {
            var response = await _client.GetAsync($"issues.json{filter}");
            var stringContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TasksResponseDto>(stringContent).Issues;
            }

            throw new HttpRequestException($"Ошибка запроса: {stringContent}");
        }

        public async Task<bool> WriteOffHours(TimeEntry[] times)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            foreach (var spentTimeDto in times)
            {
                var serialized = JsonConvert.SerializeObject(spentTimeDto, new DateTimeConverter());
                HttpContent content = new StringContent(serialized, Encoding.UTF8, MediaTypeNames.Application.Json);
                tasks.Add(_client.PostAsync("time_entries.json", content));
            }

            var result = await Task.WhenAll(tasks);

            return result.All(x => x.IsSuccessStatusCode);
        }
    }
}
