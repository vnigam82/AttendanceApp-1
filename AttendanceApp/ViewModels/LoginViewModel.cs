using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {


        public Command  _loginCommand;
        public LoginViewModel()
        {
            GetOrganizationProfile();
        }


        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(() => ExecuteLoginCommand()));
            }
        }

        public async void GetOrganizationProfile()
        {
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }
                DependencyService.Get<IProgressBar>().Show("Please wait...");
                var menuItem = await CommonMethods.GetOrganizationProfile();
                
                if (menuItem != null)
                {
                    // DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    ImageBase64 = menuItem.logo.src;
                    ImageType = menuItem.logo.type;
                    LangType = JsonConvert.DeserializeObject<Language>(menuItem.name);
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Error Loading Data",
                                            msDuration: MaterialSnackbar.DurationLong);
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
                if (!HttpRequest.CheckConnection())
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }
                if(!Validate())
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: Error,
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }

                DependencyService.Get<IProgressBar>().Show("Authenticating user...");
                //var postData= new Login() { email= "vivek@nigam.com", password="Qwerty@123"};
                var postData = new LoginRootObject()
                {
                    Credentials =new LoginModel()
                    {
                        UserName=UserName,
                        Password = Password
                    } 
                    
                };
                
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                var loginInfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl, ServiceConfigrations.Login, jsonString);
                if (loginInfo.Status)
                {
                    //var loginResult = JsonConvert.DeserializeObject<Login>(loginInfo.Result);
                    //if (loginResult != null)
                    //{
                    //    if (loginResult.status == StatusEnum.Success)
                    //    {
                    //        Console.WriteLine("Token: " + loginResult.token);
                    //        if (Rememberme)
                    //        {
                    //            UserSettings.IsLoggedIn = Rememberme;
                    //            LoginDbModel logindb = new LoginDbModel();
                    //            logindb.Email = loginResult.email;
                    //            logindb.AccessToken = loginResult.token;
                    //            await App.Database.InsertLoginDetails(logindb);
                    //            Application.Current.MainPage = new NavigationPage(new DashboardPage());
                    //        }
                    //        else
                    //        {
                    //            if (App.Current.Properties.ContainsKey("AccessToken"))
                    //                App.Current.Properties.Remove("AccessToken");
                    //            App.Current.Properties.Add("AccessToken", loginResult.token);
                    //            await App.Current.SavePropertiesAsync();
                    //            Application.Current.MainPage = new NavigationPage(new DashboardPage());
                    //        }
                    //    }
                    //}
                }
                else
                {
                    DependencyService.Get<IProgressBar>().Hide();
                    await MaterialDialog.Instance.SnackbarAsync(message: loginInfo.Message,
                                            actionButtonText: "Ok",
                                            msDuration: 3000);
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

        private bool Validate()
        {
            bool result = true;
            Error = string.Empty;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                Error += "Please provide User Name";
                result = false;
            }
            
            if (string.IsNullOrWhiteSpace(Password))
            {
                Error += "\nPlease provide Password";
                result = false;
            }
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
