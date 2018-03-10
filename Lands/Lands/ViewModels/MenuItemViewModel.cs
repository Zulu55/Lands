namespace Lands.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;
    using Helpers;

    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = string.Empty;
                mainViewModel.TokenType = string.Empty;
                Application.Current.MainPage = new NavigationPage(
                    new LoginPage());
            }
        }
        #endregion
    }
}