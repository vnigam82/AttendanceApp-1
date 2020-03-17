using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AttendanceApp.Views;
using Xamarin.Forms;

namespace AttendanceApp.ShellFiles
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public static bool back = true;
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }
        Random rand = new Random();
        public ICommand RandomPageCommand
        {
            get
            {
                return new Command<string>((pageName) => NavigateToRandomPageAsync(pageName));
            }
        }


        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        private void RegisterRoutes()
        {
            routes.Add("checkincheckout", typeof(CheckinCheckout));
            routes.Add("aboutus", typeof(AboutUs));
            routes.Add("approveleaves", typeof(ApproveLeaves));
            routes.Add("approvereasons", typeof(ApproveReasons));
            routes.Add("configurationsettings", typeof(ConfigurationSetting));
            routes.Add("dashboard", typeof(DashboardPage));
            routes.Add("leaverequest", typeof(LeaveRequest));
            routes.Add("login", typeof(Login));
            routes.Add("myattendance", typeof(MyAttendance));
            routes.Add("reasonrequest", typeof(ReasonRequest));
            routes.Add("unjustifiedviolations", typeof(UnjustifiedViolations));
            routes.Add("userprofile", typeof(UserProfile));
            routes.Add("changepassword", typeof(ChangePassword));
            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }


        public void NavigateToRandomPageAsync(string pageName)
        {

            back = true;

            switch (pageName)
            {

                    case "checkincheckout":
                    Shell.Current.GoToAsync("checkincheckout");
                    break;

                    case "reasonrequest":
                    Shell.Current.GoToAsync("reasonrequest");
                    break;
                    
                    case "aboutus":
                    Shell.Current.GoToAsync("aboutus");
                    break;
            }



            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
