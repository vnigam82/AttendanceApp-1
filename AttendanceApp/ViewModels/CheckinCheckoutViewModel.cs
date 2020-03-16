using System;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class CheckinCheckoutViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        private bool _isShowBack, _isShowMenuButton;
        #endregion
        public CheckinCheckoutViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            
        }

        public bool IsShowBack
        {
            get
            {
                return _isShowBack;
            }
            set
            {
                if (_isShowBack != value)
                {
                    _isShowBack = value;
                    OnPropertyChanged(nameof(IsShowBack));
                }
            }
        }
        public bool IsShowMenuButton
        {
            get
            {
                return _isShowMenuButton;
            }
            set
            {
                if (_isShowMenuButton != value)
                {
                    _isShowMenuButton = value;
                    OnPropertyChanged(nameof(IsShowMenuButton));
                }
            }
        }
    }
}
