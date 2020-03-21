using System;
using AttendanceApp.Dependency;
using AttendanceApp.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoadingIndicator))]
namespace AttendanceApp.iOS.Dependency
{
    public class LoadingIndicator : IProgressBar
    {
        public void Hide()
        {
            BigTed.BTProgressHUD.Dismiss();
        }

        public void Show(string message)
        {
            BigTed.BTProgressHUD.Show(message, -1, BigTed.ProgressHUD.MaskType.Black);
        }
    }
}
