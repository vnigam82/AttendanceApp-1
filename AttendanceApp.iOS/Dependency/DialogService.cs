using System;
using AttendanceApp.Dependency;
using AttendanceApp.iOS.Dependency;
using AttendanceApp.iOS.Views;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialogService))]
namespace AttendanceApp.iOS.Dependency
{
    public class DialogService : IDialogs
    {
        public void ShowLogoutPopup()
        {
            int screenwidth = (int)UIScreen.MainScreen.Bounds.Width;
            int screenheight = (int)UIScreen.MainScreen.Bounds.Height;
            CustomPopUpView cpuv = new CustomPopUpView(new CoreGraphics.CGSize((screenwidth - screenwidth / 4), 250));
            cpuv.PopUp(true, delegate {
                Console.WriteLine("cpuv will close");
            });
        }
        public void ShowPopup(string Header, string Message)
        {
            //int screenwidth = (int)UIScreen.MainScreen.Bounds.Width;
            //int screenheight = (int)UIScreen.MainScreen.Bounds.Height;
            //MessagePopUpView cpuv = new MessagePopUpView(new CoreGraphics.CGSize((screenwidth - screenwidth / 4), 250), Header, Message);
            //cpuv.PopUp(true, delegate {
            //    Console.WriteLine("cpuv will close");
            //});
        }
    }
}
