using System;
namespace AttendanceApp.Models
{
    public class Logo
    {
        public string src { get; set; }
        public string type { get; set; }
    }

    public class OrganizationProfile
    {
        public string name { get; set; }
        public Logo logo { get; set; }
    }
}
