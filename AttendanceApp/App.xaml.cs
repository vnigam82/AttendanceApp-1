using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AttendanceApp.CustomControls;
using AttendanceApp.Database;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.Resx;
using AttendanceApp.ShellFiles;
using AttendanceApp.Utils;
using AttendanceApp.ViewModels;
using AttendanceApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace AttendanceApp
{
    public partial class App : Application
    {
        public static string lang = "";
        public static string countryCode = "";
        List<clsMessages> lstMessage = new List<clsMessages>();
        public App()
        {
            InitializeComponent();

            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(netLanguage);

            lang = netLanguage;


            XF.Material.Forms.Material.Init(this);
            LoginDBModel objUser = App.Database.GetLoggedInUser();
            if (objUser != null)
            {
                //if (objUser.RememberMe)
                //{
                    MainPage = new AppShell();
                //}
                //else
                //{
                //    MainPage = new Login();
                //}
            }
            else
            {
                MainPage = new Login();
            }
        }
        public static void RegisterViewModels(INavigation navigation)
        {
            ServiceContainer.Register(() => new DashboardViewModel(navigation));
            ServiceContainer.Register(() => new HeaderViewModel(navigation));
            ServiceContainer.Register(() => new ArabicHeaderViewModel(navigation));
            ServiceContainer.Register(() => new UserProfileViewModel(navigation));
            ServiceContainer.Register(() => new ResetPasswordViewModel(navigation));
            ServiceContainer.Register(() => new CheckinCheckoutViewModel(navigation));
            ServiceContainer.Register(() => new ReasonRequestViewModel(navigation));
            ServiceContainer.Register(() => new MyAttendanceViewModel(navigation));
            ServiceContainer.Register(() => new FlyoutHeaderViewModel(navigation));

        }

        static AttendanceDBClass database;
        public static AttendanceDBClass Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new AttendanceDBClass(DependencyService.Get<IFileHelper>().GetLocalFilePath("AttendanceDb.db"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override async void OnResume()
        {
            lstMessage = new List<clsMessages>();
            var snackbarConfiguration = new MaterialSnackbarConfiguration()
            {
                TintColor = Color.White,
                MessageTextColor = HttpRequest.CheckConnection() ? Color.White : Color.Red
            };
            if (!HttpRequest.CheckConnection())
            {
                await DependencyService.Get<IXSnack>().ShowMessageAsync("You are not connected to internet.");
            }
            else
            {
                await DependencyService.Get<IXSnack>().ShowMessageAsync("You are connected to internet.");
                
                DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                var data = App.database.GetBooking();
                if (data!=null)
                {
                    if (data.Count>0)
                    {
                        try
                        {
                            await DependencyService.Get<IXSnack>().ShowMessageAsync("Syncing Data. Please wait..");
                            foreach (var item in data)
                            {
                                clsMessages messagedata = new clsMessages();
                                var locjsonStringOfflone = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationData>(item.Location);
                                var locationData = new LocationData()
                                {
                                    lat = locjsonStringOfflone.lat,
                                    lng = locjsonStringOfflone.lng,
                                    radius = locjsonStringOfflone.radius
                                };
                                var locjsonString = Newtonsoft.Json.JsonConvert.SerializeObject(locationData);

                                var postData = new List<BookingModel>()
                                            {
                                                new BookingModel()
                                                    {
                                                        DateTime=item.DateTime,
                                                        Direction=item.Direction,
                                                        Location=locjsonString,
                                                        HappinessOption=item.HappinessOption,
                                                        ReasonCode=item.ReasonCode
                                            }
                                        };
                                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                                var loginInfo = await CommonMethods.BookingAttendance(jsonString);
                                if (loginInfo.Status)
                                {
                                    messagedata.Status = loginInfo.Status;
                                    messagedata.Message = loginInfo.Message;
                                    App.database.ClearBookingBasedOnId(item);
                                    //await MaterialDialog.Instance.SnackbarAsync(message: "Data successfully synced to the server.",
                                    //msDuration: MaterialSnackbar.DurationLong);
                                    
                                }
                                else
                                {
                                    messagedata.Status = loginInfo.Status;
                                    messagedata.Message = loginInfo.Message;

                                    DependencyService.Get<IProgressBar>().Hide();
                                    //await MaterialDialog.Instance.SnackbarAsync(message: "Error syncing data to the server.",
                                      //  msDuration: MaterialSnackbar.DurationLong);
                                }
                                lstMessage.Add(messagedata);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            DependencyService.Get<IProgressBar>().Hide();
                            await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
                        }
                        finally
                        {
                            DependencyService.Get<IProgressBar>().Hide();
                            if (lstMessage.Any(x=>x.Status==false))
                            {
                                await DependencyService.Get<IXSnack>().ShowMessageAsync("Error syncing data to the server.");
                                
                            }
                            else
                            {
                                await DependencyService.Get<IXSnack>().ShowMessageAsync("Data successfully synced to the server.");
                                
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<IProgressBar>().Hide();
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DependencyService.Get<IXSnack>().ShowMessageAsync("There is no record stored locally.");

                        });
                       
                    }
                }
                else
                {
                    DependencyService.Get<IProgressBar>().Hide();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.ErrorLoadingData);
                    });
                    
                }
            }
        }
    }
}
