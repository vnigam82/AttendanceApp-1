using System;
using System.Collections.Generic;
using AttendanceApp.Views;
using Xamarin.Forms;

namespace AttendanceApp.CustomControls
{
    public partial class FlyoutHeader : ContentView
    {
        public FlyoutHeader()
        {
            InitializeComponent();
        }

        void btnEditProfile_Clicked(System.Object sender, System.EventArgs e)
        {
            
            Xamarin.Forms.Shell.Current.GoToAsync("userprofile");
            Shell.Current.FlyoutIsPresented = false;
            //Shell.FlyoutBehaviorProperty=FlyoutBehavior.Disabled;// (this,FlyoutBehavior.Flyout);
        }
    }
}
