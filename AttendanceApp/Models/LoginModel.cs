using System;
namespace AttendanceApp.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginRootObject
    {
        public LoginModel Credentials { get; set; }
    }
}
