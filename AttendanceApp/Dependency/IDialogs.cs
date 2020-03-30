using System;
namespace AttendanceApp.Dependency
{
    public interface IDialogs
    {
        void ShowLogoutPopup();
        void ShowPopup(string Header, string Message);
    }
}
