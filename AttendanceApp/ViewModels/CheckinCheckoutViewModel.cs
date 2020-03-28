using System;
using System.Collections.ObjectModel;
using AttendanceApp.CustomControls.RadioButton;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class CheckinCheckoutViewModel : BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        private bool _isShowBack, _isShowMenuButton;

        private ObservableCollection<RadioOption> _radiooptionlist;
        public ObservableCollection<RadioOption> RadioOptionsList
        {
            get { return _radiooptionlist; }
            set
            {

                _radiooptionlist = value;
            }
        }

        private ObservableCollection<clsReasons> _reasonlist;
        public ObservableCollection<clsReasons> ReasonList
        {
            get { return _reasonlist; }
            set
            {

                _reasonlist = value;
            }
        }
        #endregion
        public CheckinCheckoutViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public void InitializeRadioButtonList()
        {
            RadioOptionsList = new ObservableCollection<RadioOption>()
            {
                new RadioOption(){
                    id=1,
                    IsSelected=false,
                    Title="Happy",
                    HappynessCode=1,
                    ImageName="happyface.png"
                },
                 new RadioOption(){
                    id=2,
                    IsSelected=false,
                    Title="Sad",
                    HappynessCode=-1,
                    ImageName="sadface.png"
                },
                 new RadioOption(){
                    id=3,
                    IsSelected=false,
                    Title="Neutral",
                    HappynessCode=0,
                    ImageName="neutralface.png"
                },
            };
        }

        public async void GetReasonList()
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
                var menuItem = await CommonMethods.GetReasons();

                if (menuItem != null)
                {
                    if (menuItem.Count>0)
                    {
                        DependencyService.Get<IProgressBar>().Hide();
                        foreach (var item in menuItem)
                        {
                            var data = new clsReasons();
                            data.code = item.code;
                            data.name = item.name;
                            data.langData= JsonConvert.DeserializeObject<ReasonLanguage>(item.name);
                            ReasonList.Add(data);
                        }
                    }
                    
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

        public bool IsShowBack
        {
            get
            {
                return _isShowBack;
            }
            set
            {
                if (_isShowBack != value)
                {
                    _isShowBack = value;
                    OnPropertyChanged(nameof(IsShowBack));
                }
            }
        }
        public bool IsShowMenuButton
        {
            get
            {
                return _isShowMenuButton;
            }
            set
            {
                if (_isShowMenuButton != value)
                {
                    _isShowMenuButton = value;
                    OnPropertyChanged(nameof(IsShowMenuButton));
                }
            }
        }

        private ObservableCollection<MenuModel> _menuItems = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (_menuItems != value)
                {
                    _menuItems = value;
                    OnPropertyChanged(nameof(MenuItems));
                }
            }
        }

        public void AddMenuItems()
        {

            MenuItems.Add(new MenuModel { Id = 1, Name = "Coming (Check IN)", ImageName = "checkinwhite.png", GridColor = GridColorUtil.StoreColor });
            MenuItems.Add(new MenuModel { Id = 2, Name = "Reasons", ImageName = "reasonwhite.png", GridColor = GridColorUtil.ManualsColor });
            MenuItems.Add(new MenuModel { Id = 3, Name = "Leaving (Check Out)", ImageName = "leaveswhite.png", GridColor = GridColorUtil.AnnouncementsColor });
        }
        public Command MenuItemsCommand
        {
            get
            {
                return new Command((data) =>
                {
                    var result = data as MenuModel;
                    CheckLocation(result.Id);
                });

            }
        }
        public async void CheckLocation(int temId)
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
                var menuItem = await CommonMethods.GetLocations();

                if (menuItem.locationData != null)
                {

                    try
                    {
                        var locator = CrossGeolocator.Current;
                        var sourceLocation = await locator.GetPositionAsync();

                        if (sourceLocation != null)
                        {

                            Location sourceCoordinates = new Location(sourceLocation.Latitude, sourceLocation.Longitude);
                            Location destinationCoordinates = new Location(menuItem.locationData.lat, menuItem.locationData.lng);
                            double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                            double distanceMeter = distance * 1000;
                            if (distanceMeter < menuItem.locationData.radius)
                            {
                                await App.Current.MainPage.DisplayAlert("AttendanceApp", "You are in location", "OK");
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("AttendanceApp", "You are out of location", "OK");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                                           msDuration: MaterialSnackbar.DurationLong);
                    }
                    DependencyService.Get<IProgressBar>().Hide();
                }
                else
                {
                    DependencyService.Get<IProgressBar>().Hide();
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
    }
}
