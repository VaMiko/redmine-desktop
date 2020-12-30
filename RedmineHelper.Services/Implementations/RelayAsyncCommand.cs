namespace RedmineHelper.Services.Implementations
{
    using System;
    using System.Threading.Tasks;
    using RedmineHelper.Shared.Abstractions;
    public class RelayAsyncCommand : IAsyncCommand
    {
        private Func<object, Task> execute;
        private Func<object, bool> canExecute;

        public RelayAsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public async Task ExecuteAsync(object parameter)
        {
            await execute(parameter);
        }
    }
}
