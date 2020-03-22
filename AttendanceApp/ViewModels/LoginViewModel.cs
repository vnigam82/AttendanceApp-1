using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AttendanceApp.Database;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.ShellFiles;
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
                UserName = "JBH\\naomif";
                Password = "GAT123";
                if(!Validate())
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: Error,
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }

                DependencyService.Get<IProgressBar>().Show("Authenticating user...");
                //var postData= new Login() { email= "vivek@nigam.com", password="Qwerty@123"};
                //var postData = new LoginRootObject()
                //{
                //    Credentials =new LoginModel()
                //    {
                //        username=UserName,
                //        password = Password
                //    } 

                //};
                var postData = new LoginModel()
                {
                    username=UserName,
                    password=Password
                };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                var loginInfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl1, ServiceConfigrations.Login+ "?IsDeviceValidated=false", jsonString);
                if (loginInfo.Status)
                {
                    if (loginInfo.Result!=null)
                    {
                        UserSettingUtils.UserName = UserName;
                        UserSettingUtils.Password = Password;
                        UserSettingUtils.UserLoginGUID = loginInfo.Result.Substring(1, loginInfo.Result.Length - 2);

                        if (Rememberme)
                        {
                            var logindbdata = new LoginDBModel();
                            logindbdata.UserName = UserName;
                            logindbdata.Password = Password;
                            logindbdata.UserGUID = loginInfo.Result.Substring(1, loginInfo.Result.Length - 2);

                            App.Database.SaveLoggedInUser(logindbdata);

                            DependencyService.Get<IProgressBar>().Hide();

                            App.Current.MainPage = new AppShell();
                        }
                        else
                        {
                            App.Current.MainPage = new AppShell();
                        }
                    }
                    else
                    {
                        DependencyService.Get<IProgressBar>().Hide();
                        await MaterialDialog.Instance.SnackbarAsync(message: "Invalid User Details",
                                            actionButtonText: "Ok",
                                            msDuration: 3000);
                        
                    }

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
