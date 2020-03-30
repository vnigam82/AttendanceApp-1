using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class DashboardViewModel:BaseViewModel
    {
        private INavigation _navigation;
        private ObservableCollection<DashboardMenuModel> _dashboardItems = new ObservableCollection<DashboardMenuModel>();
        CheckinCheckoutViewModel checkinviewmodel; 
        public DashboardViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        
        public ObservableCollection<DashboardMenuModel> DashboardMenuItems
        {
            get
            {
                return _dashboardItems;
            }
            set
            {
                if (_dashboardItems != value)
                {
                    _dashboardItems = value;
                    OnPropertyChanged(nameof(DashboardMenuItems));
                }
            }
        }
        public void AddDashboardMenuItems()
        {

            DashboardMenuItems.Add(new DashboardMenuModel { Id = 1, Name = DashboardTilesText.CheckInChckout, Image = DashboardTilesImages.CheckInChckoutImage,GridColor=GridColorUtil.CheckInChckoutColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 2, Name = DashboardTilesText.LeaveRequest, Image = DashboardTilesImages.LeaveRequestImage, GridColor = GridColorUtil.LeaveRequestColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 3, Name = DashboardTilesText.ReasonRequest, Image = DashboardTilesImages.ReasonRequestImage, GridColor = GridColorUtil.ReasonRequestColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 4, Name = DashboardTilesText.UnjustifiedViolations, Image = DashboardTilesImages.UnjustifiedViolationsImage, GridColor = GridColorUtil.UnjustifiedViolationsColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 5, Name = DashboardTilesText.MyAttendance, Image = DashboardTilesImages.MyAttendanceImage, GridColor = GridColorUtil.MyAttendanceColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 6, Name = DashboardTilesText.ApproveReasons, Image = DashboardTilesImages.ApproveReasonsImage, GridColor = GridColorUtil.ApproveReasonsColor });
            DashboardMenuItems.Add(new DashboardMenuModel { Id = 7, Name = DashboardTilesText.ApproveLeaves, Image = DashboardTilesImages.ApproveLeavesImage, GridColor = GridColorUtil.ApproveLeavesColor });
        }
        public async void GetOrganizationProfile()
        {
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await CommonMethods.ShowPopup("Please check your network connection.");
                    //await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                                           // msDuration: MaterialSnackbar.DurationLong);
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
                    //await MaterialDialog.Instance.SnackbarAsync(message: "Error Loading Data",
                                            //msDuration: MaterialSnackbar.DurationLong);
                    await CommonMethods.ShowPopup("Error Loading Data.");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgressBar>().Hide();
                await CommonMethods.ShowPopup(ex.Message);
                //await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                            //msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                DependencyService.Get<IProgressBar>().Hide();
            }
        }
        public Command ItemTappedCommand
        {
            get
            {
                return new Command((data) =>
                {
                    var result = data as DashboardMenuModel;

                    if (result.Id == 1)
                    {
                        
                        Xamarin.Forms.Shell.Current.GoToAsync("checkincheckout");
                        checkinviewmodel = new CheckinCheckoutViewModel(_navigation);
                        checkinviewmodel.IsShowMenuButton = false;
                        checkinviewmodel.IsShowBack = true;
                    }
                    //if (result.Id == 2)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("twoWheelerParking");
                    //}
                    if (result.Id == 3)
                    {
                       Xamarin.Forms.Shell.Current.GoToAsync("reasonrequest");
                    }
                    //if (result.Id == 4)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("fourWheelerParking");
                    //}
                    if (result.Id == 5)
                    {
                        Xamarin.Forms.Shell.Current.GoToAsync("myattendance");
                    }
                    //if (result.Id == 6)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("gym");
                    //}
                    //if (result.Id == 7)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("sports");
                    //}
                    //if (result.Id == 8)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("swimming");
                    //}

                });
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
    }
}
