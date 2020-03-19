using System;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class MyAttendanceViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        #endregion
        public MyAttendanceViewModel(INavigation navigation)
        {
            this._navigation = navigation;

        }
    }
}
