using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class ChangePassword : ContentPage
    {
        ResetPasswordViewModel _resetPasswordViewModel;
        public ChangePassword()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _resetPasswordViewModel = ServiceContainer.Resolve<ResetPasswordViewModel>();
            //_userProfileViewmodel.AddDashboardMenuItems();
            BindingContext = _resetPasswordViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            safeInsets.Top = Device.RuntimePlatform == Device.Android ? 0 : 40;
            mainlayout.Padding = safeInsets;

        }
    }
}
