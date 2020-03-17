using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class CheckinCheckout : ContentPage
    {
        CheckinCheckoutViewModel _checkincheckoutViewmodel;
        public CheckinCheckout()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _checkincheckoutViewmodel = ServiceContainer.Resolve<CheckinCheckoutViewModel>();
            //_checkincheckoutViewmodel.AddDashboardMenuItems();
            BindingContext = _checkincheckoutViewmodel;
            //headerView.BindingContext = _checkincheckoutViewmodel;
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
