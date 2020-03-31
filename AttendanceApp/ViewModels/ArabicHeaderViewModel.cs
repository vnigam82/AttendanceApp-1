using System;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Views;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class ArabicHeaderViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        public Command _menuCommand, _backCommand, _homeCommand;
        #endregion
        public ArabicHeaderViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public Command MenuCommand
        {
            get
            {
                return _menuCommand ?? (_menuCommand = new Command(() => MenuCommandExecute()));
            }
        }

        public Command BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new Command(() => BackCommandExecute()));
            }
        }
        public Command HomeCommand
        {
            get
            {
                return _homeCommand ?? (_homeCommand = new Command(() => HomeCommandExecute()));
            }
        }

        private void HomeCommandExecute()
        {
            Shell.Current.CurrentItem = new DashboardPage();
        }

        private void BackCommandExecute()
        {
            _navigation.PopAsync();
        }

        private void MenuCommandExecute()
        {
            
            Xamarin.Forms.Shell.Current.FlowDirection = FlowDirection.RightToLeft;
            Xamarin.Forms.Shell.Current.FlyoutIsPresented = true;
        }
    }
}
