using System;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.Utils;
using AttendanceApp.Views;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class FlyoutHeaderViewModel:BaseViewModel
    {
        private INavigation _navigation;

        public FlyoutHeaderViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        internal async void GetUserProfile()
        {
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }
                //DependencyService.Get<IProgressBar>().Show("Please wait...");
                var menuItem = await CommonMethods.GetUserProfile();

                if (menuItem.Status)
                {
                    if (menuItem!=null)
                    {
                        ProfileUserName = menuItem.fullName;
                        ProfileUserEmail = menuItem.email;
                        ProfilePic = menuItem.picture ;
                    }
                    
                }
                else
                {
                    if (menuItem.StatusCode==System.Net.HttpStatusCode.NotFound)
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: menuItem.Message,
                                            msDuration: MaterialSnackbar.DurationLong);
                        App.Current.MainPage = new Login();
                    }
                    else
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: menuItem.Message,
                                            msDuration: MaterialSnackbar.DurationLong);
                    }
                }
            }
            catch (Exception ex)
            {
                //DependencyService.Get<IProgressBar>().Hide();
                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                            msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                //DependencyService.Get<IProgressBar>().Hide();
            }
        }

        private string _name;
        public string ProfileUserName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("ProfileUserName");
            }
        }

        private string _email;
        public string ProfileUserEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("ProfileUserEmail");
            }
        }

        private Logo _profilePic;
        public Logo ProfilePic
        {
            get { return _profilePic; }
            set
            {
                _profilePic = value;
                OnPropertyChanged("ProfilePic");
            }
        }
    }
}
