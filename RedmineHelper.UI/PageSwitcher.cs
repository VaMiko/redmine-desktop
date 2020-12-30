using System.Windows.Controls;
using RedmineHelper.UI.Views;
using SimpleInjector;

namespace RedmineHelper.UI
{
    public class PageSwitcher
    {
        private readonly Container _container;

        public PageSwitcher(Container container)
        {
            _container = container;
        }

        public void Switch<T>()
            where T : Page
        {
            _container.GetInstance<MainWindow>().Content = _container.GetInstance<T>();
        }
    }
}
