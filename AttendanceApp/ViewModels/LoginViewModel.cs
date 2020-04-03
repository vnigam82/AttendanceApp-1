using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AttendanceApp.Database;
using AttendanceApp.Database.DBModel;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.Resx;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.ShellFiles;
using AttendanceApp.Utils;
using AttendanceApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command _loginCommand;
        LoginDBModel objUser = new LoginDBModel();
        OrgProfileDBModel orgDetails = new OrgProfileDBModel();

        public LoginViewModel()
        {
            GetOrganizationProfile();
            objUser = App.Database.GetLoggedInUser();
            orgDetails = App.Database.GetOrganizationDetails();
            if (objUser != null)
            {
                UserName = objUser.UserName;
                Password = objUser.Password;
            }
            var sqlLiteResult = App.Database.GetLanguage();
            if (sqlLiteResult != null)
            {
                if (sqlLiteResult.LangKey == "ar-AE")
                {
                    LngToggled = true;
                    LanText = "Arabic";
                }
                else
                {
                    LngToggled = false;
                    LanText = "English";
                }
            }
            else
            {
                LngToggled = false;
                LanText = "English";
                string lang = "en-US";
                AppLanguage objUser = new AppLanguage();
                objUser.LangKey = lang;
                App.lang = lang;
                App.Database.SaveLanguage(objUser);
            }
        }
        public Command ToggledCommand
        {
            get
            {
                return new Command((data) =>
                {
                    if (LngToggled == false)
                    {
                        string lang = "en-US";
                        AppLanguage objUser = new AppLanguage();
                        objUser.LangKey = lang;
                        App.lang = lang;
                        App.Database.SaveLanguage(objUser);
                        L10n.SetLocale();
                        var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
                        AppResources.Culture = new CultureInfo(lang);
                        App.Current.MainPage = new Login();
                    }
                    else
                    {
                        string lang = "ar-AE";
                        AppLanguage objUser = new AppLanguage();
                        objUser.LangKey = lang;
                        App.lang = lang;
                        App.Database.SaveLanguage(objUser);
                        L10n.SetLocale();
                        var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
                        AppResources.Culture = new CultureInfo(lang);
                        App.Current.MainPage = new Login();
                    }
                });

            }
        }
        private bool iISBusy;
        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(() =>
                {
                    iISBusy = true;
                    ExecuteLoginCommand();
                }, () => !iISBusy));
            }
        }

        public async void GetOrganizationProfile()
        {
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    OrgProfileDBModel objUser = App.Database.GetOrganizationDetails();
                    if (objUser != null)
                    {

                        ImageBase64 = objUser.src;
                        ImageType = orgDetails.type;
                      ////  LangType = JsonConvert.DeserializeObject<Language>(orgDetails.name);
                        
                      //      Device.BeginInvokeOnMainThread(async () =>
                      //      {
                      //          await MaterialDialog.Instance.SnackbarAsync(message: Resx.AppResources.pleaseCheckYourNetworkConnection,
                      //             msDuration: MaterialSnackbar.DurationLong);
                      //      });
                        
                      


                      //  return;

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await MaterialDialog.Instance.SnackbarAsync(message: "Please login atleast first in net connectivity to get logo.",
                        msDuration: MaterialSnackbar.DurationLong);
                        });
                        return;
                    }

                }
                else
                {
                    DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                    var menuItem = await CommonMethods.GetOrganizationProfile();

                    if (menuItem != null)
                    {
                        ImageBase64 = menuItem.logo.src;
                        ImageType = menuItem.logo.type;
                        LangType = JsonConvert.DeserializeObject<Language>(menuItem.name);


                        OrgProfileDBModel orgData = new OrgProfileDBModel();
                        orgData.src = menuItem.logo.src;
                        orgData.type = menuItem.logo.type;
                        orgData.name = menuItem.name;


                        var status = App.Database.SaveOrganizationUser(orgData);
                    }
                    else
                    {
                        await CommonMethods.ShowPopup(Resx.AppResources.ErrorLoadingData);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgressBar>().Hide();
                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                  msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                DependencyService.Get<IProgressBar>().Hide();
            }
        }


        public async void ExecuteLoginCommand()
        {
            try
            {

                IsButtonDisabled = false;

              // UserName = "JBH\\naomif";
              // Password = "GAT123";
                if (!HttpRequest.CheckConnection())
                {
                    if (!Validate())
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: Error,
                         msDuration: MaterialSnackbar.DurationLong);
                        //await CommonMethods.ShowPopup(Error);
                        return;
                    }
                    else
                    {
                        if (objUser != null)
                        {
                            if (objUser.UserName == UserName && objUser.Password == Password)
                            {
                                App.Current.MainPage = new AppShell();
                            }
                            else
                            {
                                await CommonMethods.ShowPopup(Resx.AppResources.pleaseCheckYourNetworkConnection);
                                return;
                            }
                        }
                        else
                        {
                            //await CommonMethods.ShowPopup(Resx.AppResources.pleaseCheckYourNetworkConnection);
                            await MaterialDialog.Instance.SnackbarAsync(message: "Please login atleast first in net connectivity.",
                            msDuration: MaterialSnackbar.DurationLong);
                            return;
                        }
                    }

                    //await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                    //msDuration: MaterialSnackbar.DurationLong);

                }
                else
                {
                    if (!Validate())
                    {
                        await MaterialDialog.Instance.SnackbarAsync(message: Error,
                         msDuration: MaterialSnackbar.DurationLong);
                        //await CommonMethods.ShowPopup(Error);
                        return;
                    }

                    DependencyService.Get<IProgressBar>().Show(Resx.AppResources.authenticatingUser);
                    var postData = new LoginModel()
                    {
                        username = UserName.Contains("\\\\") ? UserName.Replace(@"\\", @"\") : UserName,
                        password = Password
                    };
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                    var loginInfo = await CommonMethods.LogInToUser(jsonString);
                    if (loginInfo.Status)
                    {
                        if (loginInfo.Result != null)
                        {
                            UserSettingUtils.UserName = UserName;
                            UserSettingUtils.Password = Password;
                            UserSettingUtils.UserLoginGUID = loginInfo.Result;

                            //if (Rememberme)
                            //{
                            var logindbdata = new LoginDBModel();
                            logindbdata.UserName = UserName;
                            logindbdata.Password = Password;
                            logindbdata.UserGUID = loginInfo.Result;
                            logindbdata.RememberMe = Rememberme;

                            App.Database.SaveLoggedInUser(logindbdata);

                            DependencyService.Get<IProgressBar>().Hide();

                            App.Current.MainPage = new AppShell();
                            //}
                            //else
                            //{
                            //    App.Current.MainPage = new AppShell();
                            //}
                        }
                        else
                        {
                            DependencyService.Get<IProgressBar>().Hide();
                            await CommonMethods.ShowPopup(Resx.AppResources.invalidUserDetails);
                            //await MaterialDialog.Instance.SnackbarAsync(message: "Invalid User Details",
                            // actionButtonText: "Ok",
                            // msDuration: 3000);

                        }

                    }
                    else
                    {
                        DependencyService.Get<IProgressBar>().Hide();
                        //await CommonMethods.ShowPopup(loginInfo.Message);
                        await MaterialDialog.Instance.SnackbarAsync(message: loginInfo.Message,
                        actionButtonText: "Ok",
                        msDuration: 3000);
                    }
                }

            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgressBar>().Hide();
                //await CommonMethods.ShowPopup(ex.Message);
                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                IsButtonDisabled = true;
                DependencyService.Get<IProgressBar>().Hide();
            }
        }

        private bool Validate()
        {
            bool result = true;
            Error = string.Empty;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                Error += Resx.AppResources.pleaseProvideUserName;
                result = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                Error += "\n" + Resx.AppResources.pleaseProvidePassword;
                result = false;
            }
            //if (!Rememberme)
            //{
            //    Error += "\nPlease check the remember me checkbox.";
            //    result = false;
            //}
            return result;
        }
        private string imageBase64;
        public string ImageBase64
        {
            get { return imageBase64; }
            set
            {
                imageBase64 = value;
                OnPropertyChanged("ImageBase64");
                string result = Regex.Replace(imageBase64, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                LogoImage = Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(result)));
            }
        }

        private Xamarin.Forms.ImageSource logoimage;
        public Xamarin.Forms.ImageSource LogoImage
        {
            get { return logoimage; }
            set
            {
                logoimage = value;
                OnPropertyChanged("LogoImage");
            }
        }

        private string imageType;
        public string ImageType
        {
            get { return imageType; }
            set
            {
                imageType = value;
                OnPropertyChanged("ImageType");
            }
        }

        private string languageType;
        public string LanguageType
        {
            get { return languageType; }
            set
            {
                languageType = value;
                OnPropertyChanged("LanguageType");
            }
        }

        private string lanText;
        public string LanText
        {
            get { return lanText; }
            set
            {
                lanText = value;
                OnPropertyChanged("LanText");
            }
        }


        private bool lngToggled;
        public bool LngToggled
        {
            get { return lngToggled; }
            set
            {
                lngToggled = value;
                OnPropertyChanged("LngToggled");
            }
        }



        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _token;
        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                OnPropertyChanged("Token");
            }
        }

        private bool _rememberme;
        public bool Rememberme
        {
            get { return _rememberme; }
            set
            {
                if (_rememberme != value)
                {
                    _rememberme = value;
                    OnPropertyChanged("Rememberme");
                }
            }
        }

        private bool _isbuttondisabled = true;
        public bool IsButtonDisabled
        {
            get { return _isbuttondisabled; }
            set
            {
                if (_isbuttondisabled != value)
                {
                    _isbuttondisabled = value;
                    OnPropertyChanged("IsButtonDisabled");
                }
            }
        }
        private Language langType;
        public Language LangType
        {
            get { return langType; }
            set
            {
                langType = value;
                OnPropertyChanged("LangType");
            }
        }
        public string Error = string.Empty;
    }
}
