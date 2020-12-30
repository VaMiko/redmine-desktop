using System.Threading.Tasks;
using System.Windows.Input;

namespace RedmineHelper.Shared.Abstractions
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
