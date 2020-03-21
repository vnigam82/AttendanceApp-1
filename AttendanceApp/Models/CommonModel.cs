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

    public class Language
    {
        public string en { get; set; }
        public string ar { get; set; }
        public string de { get; set; }
        public string fr { get; set; }
    }
}
