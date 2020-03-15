using System;
using System.Threading.Tasks;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class ResetPasswordViewModel:BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        public Command _updatePasswordCommand, _hideAnimationViewCommand;
        private bool _isShowAnimationView,_isShowPasswordView=true;
        #endregion
        public ResetPasswordViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public Command UpdatePasswordCommand
        {
            get
            {
                return _updatePasswordCommand ?? (_updatePasswordCommand = new Command(() => UpdatePasswordCommandExecute()));
            }
        }

        public Command HideAnimationViewCommand
        {
            get
            {
                return _hideAnimationViewCommand ?? (_hideAnimationViewCommand = new Command(() => HideAnimatioviewCommandExecute()));
            }
        }

        private void HideAnimatioviewCommandExecute()
        {
            IsShowAnimationView = false;
            IsShowPasswordView = true;
        }

        private async void UpdatePasswordCommandExecute()
        {
            IsShowAnimationView = true;
            IsShowPasswordView = false;
            await Task.Delay(6000);
            IsShowAnimationView = false;
            IsShowPasswordView = true;
        }
        public bool IsShowAnimationView
        {
            get
            {
                return _isShowAnimationView;
            }
            set
            {
                if (_isShowAnimationView != value)
                {
                    _isShowAnimationView = value;
                    OnPropertyChanged(nameof(IsShowAnimationView));
                }
            }
        }
        public bool IsShowPasswordView
        {
            get
            {
                return _isShowPasswordView;
            }
            set
            {
                if (_isShowPasswordView != value)
                {
                    _isShowPasswordView = value;
                    OnPropertyChanged(nameof(IsShowPasswordView));
                }
            }
        }
    }
}
