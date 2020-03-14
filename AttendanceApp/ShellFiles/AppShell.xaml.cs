using System;
using System.Collections.Generic;
using AttendanceApp.Views;
using Xamarin.Forms;

namespace AttendanceApp.ShellFiles
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }
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

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
