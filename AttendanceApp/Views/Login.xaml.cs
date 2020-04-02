using System;
using System.Collections.Generic;
using AttendanceApp.Helpers;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AttendanceApp.Views
{
    public partial class Login : ContentPage
    {
        LoginViewModel _loginViewmodel;
        public Login()
        {
            InitializeComponent();
           // App.Database.ClearLoginDetails();
            _loginViewmodel = new LoginViewModel();

            BindingContext = _loginViewmodel;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            mainlayout.Padding = safeInsets;
            //BindingContext = new LoginViewModel();
            //LoadAnimation();
        }

       
    }
}
