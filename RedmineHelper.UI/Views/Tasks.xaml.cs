namespace RedmineHelper.UI.Views
{
    using ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    public partial class Tasks : Page
    {
        private readonly RedmineTaskViewModel _viewModel;

        public Tasks(RedmineTaskViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            ShowsNavigationUI = false;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = _viewModel;
            TasksGrid.Visibility = Visibility.Visible;
        }
    }
}
