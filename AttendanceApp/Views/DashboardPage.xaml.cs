using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel _dashboardViewmodel;
        public DashboardPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            App.RegisterViewModels(Navigation);

            _dashboardViewmodel = ServiceContainer.Resolve<DashboardViewModel>();
            _dashboardViewmodel.AddDashboardMenuItems();
            _dashboardViewmodel.GetOrganizationProfile();
            BindingContext = _dashboardViewmodel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            safeInsets.Top=Device.RuntimePlatform== Device.Android?0:40;
            mainlayout.Padding = safeInsets;

        }
    }
}
