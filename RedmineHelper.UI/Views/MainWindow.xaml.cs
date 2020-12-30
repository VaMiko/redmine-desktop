using RedmineHelper.Services.Abstractions;
using SimpleInjector;

namespace RedmineHelper.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IAccessResolver _resolver;
        private readonly Container _container;

        public MainWindow(IAccessResolver resolver, Container container)
        {
            _resolver = resolver;
            _container = container;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            if (!_resolver.ResolveAccess())
                Content = _container.GetInstance<Login>();
            else
                Content = _container.GetInstance<Tasks>();
        }
    }
}
