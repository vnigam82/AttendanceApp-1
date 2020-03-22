using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using AttendanceApp.Views;
using Xamarin.Forms;

namespace AttendanceApp.CustomControls
{
    public partial class FlyoutHeader : ContentView
    {
        //FlyoutHeaderViewModel _flyoutViewmodel;
        public FlyoutHeader()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            App.RegisterViewModels(Navigation);

            //_flyoutViewmodel = ServiceContainer.Resolve<FlyoutHeaderViewModel>();

            //_flyoutViewmodel.GetUserProfile();
            //BindingContext = _flyoutViewmodel;
        }
        void btnEditProfile_Clicked(System.Object sender, System.EventArgs e)
        {
            
            Xamarin.Forms.Shell.Current.GoToAsync("userprofile");
            Shell.Current.FlyoutIsPresented = false;
            //Shell.FlyoutBehaviorProperty=FlyoutBehavior.Disabled;// (this,FlyoutBehavior.Flyout);
        }
    }
}
