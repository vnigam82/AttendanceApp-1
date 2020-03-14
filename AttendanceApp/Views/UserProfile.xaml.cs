using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
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
