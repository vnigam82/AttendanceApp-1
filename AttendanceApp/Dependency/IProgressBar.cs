using System;
namespace AttendanceApp.Dependency
{
    public interface IProgressBar
    {
        void Show(string message);
        void Hide();
    }
}
