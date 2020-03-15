using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class UserProfile : ContentPage
    {
        UserProfileViewModel _userProfileViewmodel;
        public UserProfile()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);

            _userProfileViewmodel = ServiceContainer.Resolve<UserProfileViewModel>();
            //_userProfileViewmodel.AddDashboardMenuItems();
            BindingContext = _userProfileViewmodel;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                configView.IsOpen = !configView.IsOpen;
            };
            grdConfiguration.GestureRecognizers.Add(tapGestureRecognizer);
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
