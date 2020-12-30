namespace RedmineHelper.ViewModels.Abstractions
{
    using RedmineHelper.CommandStorages.Abstractions;
    using RedmineHelper.States.Abstractions;
    using System.Threading.Tasks;
    public abstract class BaseViewModel<TState, TStorage>
        where TState : State
        where TStorage : CommandStorage<TState>
    {
        protected BaseViewModel(TState state, TStorage commands)
        {
            State = state;
            Commands = commands;
            Task.Run(PreLoadAction);
        }

        /// <summary>
        /// Состояние ViewModel
        /// </summary>
        public TState State { get; }

        /// <summary>
        /// Команды ViewModel
        /// </summary>
        public TStorage Commands { get; }

        /// <summary>
        /// Действие для инициализации
        /// </summary>
        protected abstract Task PreLoadAction();
    }
}
