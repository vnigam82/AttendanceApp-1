using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using AttendanceApp.Dependency;
using AttendanceApp.Droid.Dependency;
using AttendanceApp.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialogService))]
namespace AttendanceApp.Droid.Dependency
{
    public class DialogService : IDialogs
    {
        public void ShowLogoutPopup()
        {
            AlertDialog myCustomDialog = null;
            var activity = Forms.Context as Activity;
            using (var layoutInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService))
            {
                if (layoutInflater != null)
                {
                    var view = layoutInflater.Inflate(Resource.Layout.LogoutPopup, null);
                    var header = view.FindViewById<TextView>(Resource.Id.titleLogin);
                    header.Text = "Log out";
                    var message = view.FindViewById<TextView>(Resource.Id.lblMessage);
                    message.Text = "Are you sure to log out from Application?";
                    var btnYesClick = view.FindViewById<TextView>(Resource.Id.btnYes);
                    var btnNoClick = view.FindViewById<TextView>(Resource.Id.btnNo);
                    using (var builder = new AlertDialog.Builder(activity))
                    {
                        builder.SetView(view);
                        builder.SetCancelable(false);
                        myCustomDialog = builder.Create();

                        myCustomDialog.Show();
                        btnNoClick.Click += delegate
                        {
                            myCustomDialog.Hide();
                        };
                    }
                    btnYesClick.Click += delegate
                    {
                        myCustomDialog.Hide();
                        App.Database.ClearLoginDetails();
                        App.Current.MainPage = new Login();
                    };
                }
            }
        }

        public void ShowPopup(string Header, string Message)
        {
            //AlertDialog myCustomDialog = null;
            //var activity = Forms.Context as Activity;
            //using (var layoutInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService))
            //{
            //    if (layoutInflater != null)
            //    {
            //        var view = layoutInflater.Inflate(Resource.Layout.MessagePopup, null);
            //        var header = view.FindViewById<TextView>(Resource.Id.titleLogin);
            //        header.Text = Header;
            //        var message = view.FindViewById<TextView>(Resource.Id.lblMessage);
            //        message.Text = Message;
            //        //   var btnYesClick = view.FindViewById<TextView>(Resource.Id.btnYes);
            //        var btnNoClick = view.FindViewById<TextView>(Resource.Id.btnNo);
            //        using (var builder = new AlertDialog.Builder(activity))
            //        {
            //            builder.SetView(view);
            //            builder.SetCancelable(false);
            //            myCustomDialog = builder.Create();

            //            myCustomDialog.Show();
            //            btnNoClick.Click += delegate
            //            {
            //                myCustomDialog.Hide();
            //            };
            //        }
            //        //btnYesClick.Click += delegate
            //        //{
            //        //    myCustomDialog.Hide();
            //        //    App.Database.ClearLoginDetails();
            //        //    App.Current.MainPage = new LoginPage();
            //        //};
            //    }
            //}
        }
    }
}
