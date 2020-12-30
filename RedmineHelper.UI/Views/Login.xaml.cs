using System.Windows;
using System.Windows.Controls;
using RedmineHelper.Models.Dto;
using RedmineHelper.Services.Abstractions;
using SimpleInjector;

namespace RedmineHelper.UI.Views
{
    public partial class Login : Page
    {
        private readonly IAccessResolver _accessResolver;
        private readonly PageSwitcher _switcher;

        public Login(IAccessResolver resolver, PageSwitcher switcher)
        {
            _accessResolver = resolver;
            _switcher = switcher;
            InitializeComponent();
            ShowsNavigationUI = false;
        }

        private async void SaveKeyButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (await _accessResolver.SaveKey(new RedmineApiKey {Key = ApiBox.Password}))
                _switcher.Switch<Tasks>();
        }
    }
}
