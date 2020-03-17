using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class ReasonRequest : ContentPage
    {
        ReasonRequestViewModel _reasonRequestViewmodel;
        public ReasonRequest()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _reasonRequestViewmodel = ServiceContainer.Resolve<ReasonRequestViewModel>();
            //_reasonRequestViewmodel.AddMenuItems();
            BindingContext = _reasonRequestViewmodel;
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
