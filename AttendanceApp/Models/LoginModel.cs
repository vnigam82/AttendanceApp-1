using System;
namespace AttendanceApp.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginRootObject
    {
        public LoginModel Credentials { get; set; }
    }
}
