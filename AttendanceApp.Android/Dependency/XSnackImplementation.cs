using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using Android.Support.Design.Widget;
using Android.Views;
using AttendanceApp.CustomControls;
using AttendanceApp.Droid.Dependency;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(XSnackImplementation))]
namespace AttendanceApp.Droid.Dependency
{
    /// <summary>
    /// Android implementation for Plugin.XSnack
    /// </summary>
    public class XSnackImplementation : IXSnack 
    {
        /// <summary>
        /// Displays a message and an optional action button within a snackbar
        /// </summary>
        /// <param name="message">The message you want to be displayed</param>
        /// <param name="duration">Duration of the snackbar in seconds (Default 3 seconds)</param>
        /// <param name="actionText">Text for the action button (optional)</param>
        /// <param name="actionCommand">Callback command for the action button (optional)</param>
        /// <returns></returns>
        public Task ShowMessageAsync(string message, int duration = 3, string actionText = null, ICommand actionCommand = null)
        {
            DisplayMessage(message, duration, actionText, actionCommand);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Displays a message and an optional action button within a snackbar
        /// </summary>
        /// <param name="message">The message you want to be displayed</param>
        /// <param name="duration">Duration of the snackbar in seconds (Default 3 seconds)</param>
        /// <param name="actionText">Text for the action button (optional)</param>
        /// <param name="actionCommand">Callback command for the action button (optional)</param>
        /// <returns></returns>
        public void ShowMessage(string message, int duration = 3, string actionText = null, ICommand actionCommand = null)
            => DisplayMessage(message, duration, actionText, actionCommand);

        void DisplayMessage(string message, int duration, string actionText, ICommand actionCommand)
        {
            var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context as Activity;

            var view = ((Activity)Forms.Context).FindViewById(Android.Resource.Id.Content);

            var snack = Snackbar.Make(view, message, Snackbar.LengthIndefinite);


            snack.SetDuration(duration * 1000);
            if (actionCommand != null)
                snack.SetAction(actionText, (view) =>
                {
                    if (actionCommand?.CanExecute(null) == true)
                        actionCommand.Execute(null);
                });

            snack.Show();
        }
    }
}
