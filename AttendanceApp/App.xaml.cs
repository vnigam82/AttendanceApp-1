using System;
using AttendanceApp.ShellFiles;
using AttendanceApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AttendanceApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            //MainPage = new Login();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
