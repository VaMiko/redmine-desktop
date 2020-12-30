namespace RedmineHelper.States
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Models.Dto;
    using Shared;
    using Abstractions;
    public class TasksState : State
    {
        /// <summary>
        /// Все задачи
        /// </summary>
        public List<RedmineTaskDto> AllTasks = new List<RedmineTaskDto>();

        /// <summary>
        /// Номер страницы
        /// </summary>
        public NotifyProperty<int> PageNumber { get; } = new NotifyProperty<int>(0);

        /// <summary>
        /// Задач на странице
        /// </summary>
        public NotifyProperty<int> TasksOnPage { get; } = new NotifyProperty<int>(10);

        /// <summary>
        /// Всего страниц
        /// </summary>
        public NotifyProperty<int> TotalPages { get; } = new NotifyProperty<int>(0);

        /// <summary>
        /// Задачи на странице
        /// </summary>
        public NotifyProperty<List<RedmineTaskDto>> PageTasks { get; } = new NotifyProperty<List<RedmineTaskDto>>(new List<RedmineTaskDto>());

        /// <summary>
        /// Список задач для списания
        /// </summary>
        public NotifyProperty<ObservableCollection<ExtendedRedmineTaskDto>> SelectedTasks { get; } = new NotifyProperty<ObservableCollection<ExtendedRedmineTaskDto>>(
            new ObservableCollection<ExtendedRedmineTaskDto>());

        /// <summary>
        /// Видимость списка задач для списания
        /// </summary>
        public NotifyProperty<bool> SelectedTasksVisibility { get; } = new NotifyProperty<bool>(false);

        /// <summary>
        /// Следующая страница
        /// </summary>
        public NotifyProperty<bool> HasNext { get; } = new NotifyProperty<bool>(true);

        /// <summary>
        /// Предыдущая страница
        /// </summary>
        public NotifyProperty<bool> HasPrev { get; } = new NotifyProperty<bool>(true);

        /// <summary>
        /// Режим списания трудозатрат
        /// </summary>
        public NotifyProperty<bool> WriteOffMode { get; } = new NotifyProperty<bool>(false);

        protected override void RegisterDependencies()
        {
            PageNumber
                .AddDependencies((sender, args) =>
                {
                    HasPrev.Value = PageNumber.Value > 1;

                    HasNext.Value = TotalPages.Value == 0 ||
                                    PageNumber.Value <= TotalPages.Value;

                    PageTasks.Value = AllTasks.Skip((PageNumber.Value - 1) * TasksOnPage.Value).Take(TasksOnPage.Value)
                        .ToList();
                });

            WriteOffMode
                .AddDependencies((sender, args) =>
                {
                    SelectedTasksVisibility.Value = WriteOffMode.Value && SelectedTasks.Value.Count >= 1;
                });

            SelectedTasks
                .AddDependencies((sender, args) =>
                {
                    SelectedTasksVisibility.Value = SelectedTasks.Value.Count >= 1;
                });
        }
    }
}
