using System;
using System.Collections.ObjectModel;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
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
        #endregion
        public CheckinCheckoutViewModel(INavigation navigation)
        {
            this._navigation = navigation;

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
