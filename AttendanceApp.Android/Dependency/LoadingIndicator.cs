using System;
using AndroidHUD;
using AttendanceApp.Dependency;
using AttendanceApp.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoadingIndicator))]
namespace AttendanceApp.Droid.Dependency
{
    public class LoadingIndicator : IProgressBar
    {
        public void Show(string message)
        {
            AndHUD.Shared.Show(Forms.Context, message, -1, MaskType.Black);
        }
        public void Hide()
        {
            AndHUD.Shared.Dismiss();
        }
    }


}
