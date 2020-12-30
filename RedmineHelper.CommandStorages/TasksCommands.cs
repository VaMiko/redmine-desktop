using System.Windows;
using AutoMapper.QueryableExtensions;

namespace RedmineHelper.CommandStorages
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abstractions;
    using RedmineHelper.Mapper.Abstractions;
    using Models.Dto;
    using RedmineHelper.Services.Abstractions;
    using Services.Filters;
    using Services.Implementations;
    using States;
    public class TasksCommands : CommandStorage<TasksState>
    {
        private readonly ITaskAdapter _adapter;
        private readonly BaseMapper _mapper;

        public TasksCommands(ITaskAdapter adapter,TasksState state, BaseMapper mapper)
            : base(state)
        {
            _adapter = adapter;
            _mapper = mapper;
        }

        protected override void InitCommands()
        {
            AddCommand("AddTaskForWriteOff", new RelayAsyncCommand(obj =>
            {
                var task = _mapper.Map<RedmineTaskDto, ExtendedRedmineTaskDto>(obj);

                var existedtasks = State.SelectedTasks.Value.Select(x => x.Id).ToHashSet();
                if(!existedtasks.Contains(task.Id))
                    State.SelectedTasks.Value.Add(task);
                return Task.CompletedTask;
            }));

            AddCommand("WriteOffMode", new RelayAsyncCommand(obj =>
            {
                State.WriteOffMode.Value = !State.WriteOffMode.Value;
                return Task.CompletedTask;
            }));

            AddCommand("WriteOff", new RelayAsyncCommand(async obj =>
                {
                    if (!State.SelectedTasks.Value.Any()) return;

                    var tasksForWriteOff = State.SelectedTasks.Value
                        .AsQueryable()
                        .ProjectTo<SpentTimeDto>(_mapper.Provider)
                        .Select(x => new TimeEntry {data = x})
                        .ToArray();

                    var result = await _adapter.WriteOffHours(tasksForWriteOff);

                    MessageBox.Show(result ? "Часы списаны" : "Ошибка");
                }));

            AddCommand("NextPage", new RelayAsyncCommand(async obj =>
            {
                if (State.TotalPages.Value == 0)
                {
                    var filter = GetTaskFilter();
                    var tasks = await _adapter.GetTasks(filter);
                    if (!tasks.Any())
                    {
                        State.TotalPages.Value = State.PageNumber.Value;
                        return;
                    }

                    State.AllTasks.AddRange(tasks);

                    if (tasks.Length < filter.Limit)
                        State.TotalPages.Value = State.PageNumber.Value;
                }

                State.PageNumber.Value++;

                TaskFilter GetTaskFilter() => new TaskFilter
                    {
                        Offset = State.TasksOnPage.Value * State.PageNumber.Value,
                        Limit = State.TasksOnPage.Value
                    };
            }));

            AddCommand("PrevPage", new RelayAsyncCommand(obj =>
            {
                if (State.PageNumber.Value >= 2)
                    State.PageNumber.Value--;
                return Task.CompletedTask;
            }));
        }
    }
}
