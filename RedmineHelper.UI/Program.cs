using RedmineHelper.UI.Extensions;

namespace RedmineHelper.UI
{
    using System;
    using Views;
    using SimpleInjector;
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            Run(InitContainer());
        }

        private static Container InitContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = ScopedLifestyle.Flowing;
            container.RegisterViews();
            container.RegisterServices();
            container.Verify();

            return container;
        }

        private static void Run(Container container)
        {
            try
            {
                var app = container.GetInstance<App>();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}
