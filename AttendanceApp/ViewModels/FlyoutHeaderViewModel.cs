using System;
using AttendanceApp.CustomControls;
using AttendanceApp.Database;
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
        private string _imageSource = string.Empty;
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
                    try
                    {
                        clsDBUserProfile objUser = App.Database.GetUserProfileDetails();
                        if (objUser!=null)
                        {
                            ProfileUserName = objUser?.fullName;
                            ProfileUserEmail = objUser?.email;
                            ImageSource = objUser?.ImageSource;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                     
                    return;
                }
                else
                {
                    var menuItem = await CommonMethods.GetUserProfile();

                    if (menuItem.Status)
                    {
                        if (menuItem != null)
                        {
                            ProfileUserName = menuItem.fullName;
                            ProfileUserEmail = menuItem.email;
                            ProfilePic = menuItem.picture;
                            ImageSource= menuItem.picture.ImageSource;


                            clsDBUserProfile orgData = new clsDBUserProfile();
                            orgData.fullName = menuItem.fullName;
                            orgData.email = menuItem.email;
                            orgData.src = menuItem.picture.ImageSource;
                            orgData.ImageSource= menuItem.picture.ImageSource;
                            var status = App.Database.SaveUserProfileDetails(orgData);

                        }

                    }
                    else
                    {
                        if (menuItem.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            //await DependencyService.Get<IXSnack>().ShowMessageAsync(menuItem.Message);
                            App.Current.MainPage = new Login();
                        }
                        else
                        {
                            //await DependencyService.Get<IXSnack>().ShowMessageAsync(menuItem.Message);
                        }
                    }
                }
               
                
            }
            catch (Exception ex)
            {
                //await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
            }
            finally
            {
                
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

       
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
    }
}
