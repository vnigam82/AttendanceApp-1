using System;
using AttendanceApp.Database;
using AttendanceApp.Dependency;
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
            MainPage = new Login();
            //MainPage = new AppShell();
        }
        public static void RegisterViewModels(INavigation navigation)
        {
            ServiceContainer.Register(() => new DashboardViewModel(navigation));
            ServiceContainer.Register(() => new HeaderViewModel(navigation));
            ServiceContainer.Register(() => new UserProfileViewModel(navigation));
            ServiceContainer.Register(() => new ResetPasswordViewModel(navigation));
            ServiceContainer.Register(() => new CheckinCheckoutViewModel(navigation));
            ServiceContainer.Register(() => new ReasonRequestViewModel(navigation));
            ServiceContainer.Register(() => new MyAttendanceViewModel(navigation));
        }

        static AttendanceDBClass database;
        public static AttendanceDBClass Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new AttendanceDBClass(DependencyService.Get<IFileHelper>().GetLocalFilePath("AttendanceDb.db"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
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
