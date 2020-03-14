using System;
using AttendanceApp.Helpers;
using AttendanceApp.ShellFiles;
using AttendanceApp.ViewModels;
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
        public static void RegisterViewModels(INavigation navigation)
        {
            ServiceContainer.Register(() => new DashboardViewModel(navigation));
            ServiceContainer.Register(() => new HeaderViewModel(navigation));

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
