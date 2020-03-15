using System;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class UserProfileViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        public Command _navigateChangePasswordCommand, _navigateConfigurationCommand;
        #endregion
        public UserProfileViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public Command NavigateChangePasswordCommand
        {
            get
            {
                return _navigateChangePasswordCommand ?? (_navigateChangePasswordCommand = new Command(() => NavigateChangePasswordCommandExecute()));
            }
        }

        private void NavigateChangePasswordCommandExecute()
        {
            Xamarin.Forms.Shell.Current.GoToAsync("changepassword");
        }

        public Command NavigateConfigurationCommand
        {
            get
            {
                return _navigateConfigurationCommand ?? (_navigateConfigurationCommand = new Command(() => NavigateConfigurationCommandExecute()));
            }
        }

        private void NavigateConfigurationCommandExecute()
        {
            Xamarin.Forms.Shell.Current.GoToAsync("configurationsettings");
        }
    }
}
