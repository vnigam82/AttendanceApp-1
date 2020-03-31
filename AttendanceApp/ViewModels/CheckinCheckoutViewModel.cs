﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AttendanceApp.CustomControls.RadioButton;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private bool _isShowBack, _isShowMenuButton,_isUserExist,_isAccordianOpen,_isenabledsubmitbutton=true;
        public Command _submitCommand;
        private string _direction = string.Empty,Error = string.Empty;
        private RadioOption _selectedHappinessOption;
        private Location _latlonglocation;
        private double _radius = 0;
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
                OnPropertyChanged(nameof(ReasonList));
            }
        }

        public RadioOption SelectedHappinessOption
        {
            get { return _selectedHappinessOption; }
            set
            {

                _selectedHappinessOption = value;
                OnPropertyChanged(nameof(SelectedHappinessOption));
            }
        }
        public Location LatLongLocation
        {
            get { return _latlonglocation; }
            set
            {

                _latlonglocation = value;
                OnPropertyChanged(nameof(LatLongLocation));
            }
        }

        public double Radius
        {
            get { return _radius; }
            set
            {

                _radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }
        #endregion
        public CheckinCheckoutViewModel(INavigation navigation)
        {
            this._navigation = navigation;
        }

        public Command SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new Command(() => ExecuteSubmitCommand()));
            }
        }

        private bool Validate()
        {
            bool result = true;
            Error = string.Empty;
            if (string.IsNullOrWhiteSpace(Direction))
            {
                Error += Resx.AppResources.pleaseChooseDirection;
                result = false;
            }

            if (string.IsNullOrWhiteSpace(SelectedHappinessOption?.Title))
            {
                Error += "\n"+ Resx.AppResources.pleaseSelectHappinessOption;
                result = false;
            }

            if (string.IsNullOrWhiteSpace(SelectedReason?.name))
            {
                Error += "\n" + Resx.AppResources.pleaseSelectReason;
                result = false;
            }
            return result;
        }
        public async void ExecuteSubmitCommand()
        {
            //var data = selectedReason;
            IsEnabledSubmitButton = false;
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    await CommonMethods.ShowPopup(Resx.AppResources.pleaseCheckYourNetworkConnection);
                    //await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                    //                        msDuration: MaterialSnackbar.DurationLong);
                    return;
                }
              
                if (!Validate())
                {
                    //await MaterialDialog.Instance.SnackbarAsync(message: Error,
                    //                        msDuration: MaterialSnackbar.DurationLong);
                    await CommonMethods.ShowPopup(Error);
                    return;
                }

                DependencyService.Get<IProgressBar>().Show(Resx.AppResources.authenticatingUser);
               
                var locationData = new LocationData()
                {
                    lat = LatLongLocation.Latitude,
                    lng = LatLongLocation.Longitude,
                    radius = Radius
                };
                var locjsonString = Newtonsoft.Json.JsonConvert.SerializeObject(locationData);

                var postData = new List<BookingModel>()
                {
                    new BookingModel()
                    {
                        DateTime=DateTime.Now,
                        Direction=Direction,
                        Location=locjsonString,
                        HappinessOption=SelectedHappinessOption?.HappynessCode,
                        ReasonCode=SelectedReason?.code
                    }
                };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                var loginInfo = await CommonMethods.BookingAttendance(jsonString);
                if (loginInfo.Status)
                {
                    IsUserExist = true;
                    await CommonMethods.ShowPopup(Resx.AppResources.youAreChecked + " " + Direction + " "+ Resx.AppResources.successfully);
                    //await App.Current.MainPage.DisplayAlert("AttendanceApp", @"You are Checked "+Direction+" successfully", "OK");
                    await _navigation.PopAsync();
                }
                else
                {
                    DependencyService.Get<IProgressBar>().Hide();
                    await CommonMethods.ShowPopup(loginInfo.Message);
                    //await MaterialDialog.Instance.SnackbarAsync(message: loginInfo.Message,
                    //                        actionButtonText: "Ok",
                    //                        msDuration: 3000);
                    IsUserExist = false;
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgressBar>().Hide();
                IsUserExist = false;
                await CommonMethods.ShowPopup(ex.Message);
                //await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                //                            msDuration: MaterialSnackbar.DurationLong);
            }
            finally
            {
                IsUserExist = false;
                IsEnabledSubmitButton = true;
                DependencyService.Get<IProgressBar>().Hide();
            }
        }

        public void InitializeRadioButtonList()
        {
            RadioOptionsList = new ObservableCollection<RadioOption>()
            {
                new RadioOption(){
                    id=1,
                    IsSelected=false,
                    Title=Resx.AppResources.happy,
                    HappynessCode=1,
                    ImageName="happyface.png"
                },
                 new RadioOption(){
                    id=2,
                    IsSelected=false,
                    Title=Resx.AppResources.sad,
                    HappynessCode=-1,
                    ImageName="sadface.png"
                },
                 new RadioOption(){
                    id=3,
                    IsSelected=false,
                    Title=Resx.AppResources.neutral,
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
                    await MaterialDialog.Instance.SnackbarAsync(message: Resx.AppResources.pleaseCheckYourNetworkConnection,
                                            msDuration: MaterialSnackbar.DurationLong);
                    return;
                }
                DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                var menuItem = await CommonMethods.GetReasons();
                ReasonList = new ObservableCollection<clsReasons>();
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
                            var name = "{" + item.name + "}";
                            data.langData= JsonConvert.DeserializeObject<ReasonLanguage>(name);
                            ReasonList.Add(data);
                        }
                    }
                    
                }
                else
                {
                    await MaterialDialog.Instance.SnackbarAsync(message: Resx.AppResources.ErrorLoadingData,
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

        clsReasons selectedReason;
        public clsReasons SelectedReason
        {
            get { return selectedReason; }
            set
            {
                if (selectedReason != value)
                {
                    selectedReason = value;
                    OnPropertyChanged("SelectedReason");
                }
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
        public bool IsEnabledSubmitButton
        {
            get
            {
                return _isenabledsubmitbutton;
            }
            set
            {
                if (_isenabledsubmitbutton != value)
                {
                    _isenabledsubmitbutton = value;
                    OnPropertyChanged(nameof(IsEnabledSubmitButton));
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

        public bool IsAccordianOpen
        {
            get
            {
                return _isAccordianOpen;
            }
            set
            {
                if (_isAccordianOpen != value)
                {
                    _isAccordianOpen = value;
                    OnPropertyChanged(nameof(IsAccordianOpen));
                }
            }
        }

        public bool IsUserExist
        {
            get
            {
                return _isUserExist;
            }
            set
            {
                if (_isUserExist != value)
                {
                    _isUserExist = value;
                    OnPropertyChanged(nameof(IsUserExist));
                }
            }
        }
        public string Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    OnPropertyChanged(nameof(Direction));
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

            MenuItems.Add(new MenuModel { Id = 1, Name = Resx.AppResources.comingCheckIn, ImageName = "checkinwhite.png", GridColor = GridColorUtil.StoreColor });
            MenuItems.Add(new MenuModel { Id = 2, Name = Resx.AppResources.reasons, ImageName = "reasonwhite.png", GridColor = GridColorUtil.ManualsColor });
            MenuItems.Add(new MenuModel { Id = 3, Name = Resx.AppResources.leavingCheckOut, ImageName = "leaveswhite.png", GridColor = GridColorUtil.AnnouncementsColor });
        }
        public Command MenuItemsCommand
        {
            get
            {
                return new Command((data) =>
                {
                    var result = data as MenuModel;
                    //CheckLocation(result.Id);
                });

            }
        }
        public async void CheckLocation()
        {
            try
            {
                if (!HttpRequest.CheckConnection())
                {
                    //await MaterialDialog.Instance.SnackbarAsync(message: "Please check your network connection.",
                    //                        msDuration: MaterialSnackbar.DurationLong);
                    await CommonMethods.ShowPopup(Resx.AppResources.pleaseCheckYourNetworkConnection);
                    return;

                }
                DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                var menuItem = await CommonMethods.GetLocations();

                if (menuItem.locationData != null)
                {

                    try
                    {
                        var locator = CrossGeolocator.Current;
                        if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled)
                        {
                            var sourceLocation = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                            if (sourceLocation != null)
                            {

                                Location sourceCoordinates = new Location(sourceLocation.Latitude, sourceLocation.Longitude);
                                LatLongLocation = sourceCoordinates;
                                Location destinationCoordinates = new Location(menuItem.locationData.lat, menuItem.locationData.lng);
                                double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                                double distanceMeter = distance * 1000;
                                Radius = distanceMeter;
                                if (distanceMeter < menuItem.locationData.radius)
                                {


                                    await App.Current.MainPage.DisplayAlert("AttendanceApp", Resx.AppResources.youAreInLocation, Resx.AppResources.ok);
                                    IsUserExist = true;
                                    IsAccordianOpen = !IsAccordianOpen;
                                }
                                else
                                {

                                    await App.Current.MainPage.DisplayAlert("AttendanceApp", Resx.AppResources.youAreOutOfLocation, Resx.AppResources.ok);
                                    IsUserExist = false;
                                }
                            }
                        }
                        else
                        {
                            await CommonMethods.ShowPopup(Resx.AppResources.pleaseEnableYourLocationService);
                            return;
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        IsUserExist = false;
                        //await CommonMethods.ShowPopup(ex.Message);

                        await MaterialDialog.Instance.SnackbarAsync(message: ex.Message,
                        msDuration: MaterialSnackbar.DurationLong);
                    }
                    DependencyService.Get<IProgressBar>().Hide();
                }
                else
                {
                    IsUserExist = false;
                    DependencyService.Get<IProgressBar>().Hide();
                    await CommonMethods.ShowPopup(Resx.AppResources.ErrorLoadingData);
                    //await MaterialDialog.Instance.SnackbarAsync(message: "Error Loading Data",
                    //                        msDuration: MaterialSnackbar.DurationLong);
                }
            }
            catch (Exception ex)
            {
                IsUserExist = false;
                DependencyService.Get<IProgressBar>().Hide();
                //await CommonMethods.ShowPopup(ex.Message);
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
