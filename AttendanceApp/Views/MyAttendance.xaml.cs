using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class MyAttendance : ContentPage
    {
        MyAttendanceViewModel _myattendanceViewmodel;
        public MyAttendance()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _myattendanceViewmodel = ServiceContainer.Resolve<MyAttendanceViewModel>();
             
            BindingContext = _myattendanceViewmodel;

            //headerView.BindingContext = _checkincheckoutViewmodel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           // _myattendanceViewmodel.IsEnableSearchButton = true;
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            safeInsets.Top = Device.RuntimePlatform == Device.Android ? 0 : 40;
            mainlayout.Padding = safeInsets;

        }
    }
}
