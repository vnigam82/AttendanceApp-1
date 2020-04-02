using System;
using AttendanceApp.Helpers;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class ReasonRequestViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        private double _lablefontsize = 0;
        #endregion
        public ReasonRequestViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            LabelFontSize = CommonMethods.GetFontSizeBasedOnScreenHeight();

        }
        public double LabelFontSize
        {
            get { return _lablefontsize; }
            set
            {

                _lablefontsize = value;
                OnPropertyChanged(nameof(LabelFontSize));
            }
        }
    }
}
