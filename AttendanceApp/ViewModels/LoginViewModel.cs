using System;
using System.IO;
using System.Text.RegularExpressions;
using AttendanceApp.Helpers;
using AttendanceApp.Utils;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public LoginViewModel()
        {
            GetOrganizationProfile();
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

                var menuItem = await CommonMethods.GetOrganizationProfile();
                if (menuItem != null)
                {
                    // DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    ImageBase64 = menuItem.logo.src;
                    ImageType = menuItem.logo.type;
                    LanguageType = menuItem.name;
                }
                else
                {
                    // DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    //DependencyService.Get<IMessage>().ShortAlert("Lunch menu not found");


                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                            msDuration: MaterialSnackbar.DurationLong);
            }
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
    }
}
