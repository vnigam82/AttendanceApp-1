using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class HeaderViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        public Command _menuCommand;
        #endregion
        public HeaderViewModel(INavigation navigation)
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

        private void MenuCommandExecute()
        {
            Xamarin.Forms.Shell.Current.FlyoutIsPresented = true;
        }
    }
}
