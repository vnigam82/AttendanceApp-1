using System;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class ReasonRequestViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        #endregion
        public ReasonRequestViewModel(INavigation navigation)
        {
            this._navigation = navigation;

        }
    }
}
