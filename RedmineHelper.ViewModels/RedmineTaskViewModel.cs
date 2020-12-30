using RedmineHelper.ViewModels.Abstractions;

namespace RedmineHelper.ViewModels
{
    using States;
    using System.Threading.Tasks;
    using CommandStorages;
    using Shared.Abstractions;

    public class RedmineTaskViewModel : BaseViewModel<TasksState, TasksCommands>
    {
        public RedmineTaskViewModel(TasksState state, TasksCommands commands) :
            base(state, commands)
        {
        }

        protected override Task PreLoadAction() => ((IAsyncCommand)Commands["NextPage"]).ExecuteAsync(null);
    }
}
