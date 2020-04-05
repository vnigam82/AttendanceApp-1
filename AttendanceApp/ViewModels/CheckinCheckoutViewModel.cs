using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApp.CustomControls;
using AttendanceApp.CustomControls.RadioButton;
using AttendanceApp.Database;
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
        private bool _isShowBack, _isShowMenuButton, _isUserExist, _isAccordianOpen, _isenabledsubmitbutton = true;
        public Command _submitCommand;
        private string _direction = string.Empty, Error = string.Empty;
        private RadioOption _selectedHappinessOption;
        private Location _latlonglocation;
        private double _radius = 0, _lablefontsize = 0, _gridheaderrowfontsize = 0;
        private ObservableCollection<RadioOption> _radiooptionlist;
        List<clsMessages> lstmessage = new List<clsMessages>();
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

        public double LabelFontSize
        {
            get { return _lablefontsize; }
            set
            {

                _lablefontsize = value;
                OnPropertyChanged(nameof(LabelFontSize));
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
            lstmessage = new List<clsMessages>();
            LabelFontSize = CommonMethods.GetFontSizeBasedOnScreenHeight();
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
                Error += "\n" + Resx.AppResources.pleaseSelectHappinessOption;
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
            IsEnabledSubmitButton = false;
            try
            {
                if (!Validate())
                {
                    await DependencyService.Get<IXSnack>().ShowMessageAsync(Error);
                    return;
                }
                if (!HttpRequest.CheckConnection())
                {
                    //DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                    using (await MaterialDialog.Instance.LoadingDialogAsync(message: Resx.AppResources.pleaseWait))
                    {
                        var locationDataOfflone = new LocationData()
                        {
                            lat = LatLongLocation.Latitude,
                            lng = LatLongLocation.Longitude,
                            radius = Radius
                        };
                        var locjsonStringOfflone = Newtonsoft.Json.JsonConvert.SerializeObject(locationDataOfflone);

                        var postDataOfflone = new List<BookingModel>()
                    {
                        new BookingModel()
                        {
                            DateTime=DateTime.Now,
                            Direction=Direction,
                            Location=locjsonStringOfflone,
                            HappinessOption=SelectedHappinessOption?.HappynessCode,
                            ReasonCode=SelectedReason?.code
                        }
                    };
                        foreach (var item in postDataOfflone)
                        {
                            DBBookingModel dbdata = new DBBookingModel();
                            dbdata.DateTime = item.DateTime;
                            dbdata.Direction = item.Direction;
                            dbdata.HappinessOption = item.HappinessOption;
                            dbdata.Location = item.Location;
                            dbdata.ReasonCode = item.ReasonCode;
                            App.Database.SaveBooking(dbdata);
                        }


                        IsUserExist = true;
                        //DependencyService.Get<IProgressBar>().Hide();

                       
                    }
                    await _navigation.PopAsync();
                    await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.youAreChecked + " " + Direction + " " + Resx.AppResources.successfully);

                    return;
                }


                using (await MaterialDialog.Instance.LoadingDialogAsync(message: Resx.AppResources.pleaseWait))
                {
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
                        await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.youAreChecked + " " + Direction + " " + Resx.AppResources.successfully);
                        await _navigation.PopAsync();
                    }
                    else
                    {
                        //DependencyService.Get<IProgressBar>().Hide();
                        await DependencyService.Get<IXSnack>().ShowMessageAsync(loginInfo.Message);
                        IsUserExist = false;
                    }
                }
                //DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);

               
            }
            catch (Exception ex)
            {
                //DependencyService.Get<IProgressBar>().Hide();
                IsUserExist = false;
                await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
            }
            finally
            {
                IsUserExist = false;
                IsEnabledSubmitButton = true;
                //DependencyService.Get<IProgressBar>().Hide();
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
                ReasonList = new ObservableCollection<clsReasons>();
                if (!HttpRequest.CheckConnection())
                {
                    var objReason = App.Database.GetReason();
                    if (objReason.Count > 0)
                    {
                        foreach (var item in objReason)
                        {
                            var data = new clsReasons();
                            data.code = item.code;
                            data.name = item.name;
                            var name = "{" + item.name + "}";
                            data.langData = JsonConvert.DeserializeObject<ReasonLanguage>(name);
                            ReasonList.Add(data);
                        }
                    }
                    else
                    {
                        await DependencyService.Get<IXSnack>().ShowMessageAsync("Please get Reason atleast first in net connectivity.");
                         
                    }
                    return;
                }
                DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                var menuItem = await CommonMethods.GetReasons();

                if (menuItem != null)
                {
                    if (menuItem.Count > 0)
                    {
                        App.Database.ClearReason();

                        DependencyService.Get<IProgressBar>().Hide();

                        foreach (var item in menuItem)
                        {
                            var data = new clsReasons();
                            data.code = item.code;
                            data.name = item.name;
                            var name = "{" + item.name + "}";
                            data.langData = JsonConvert.DeserializeObject<ReasonLanguage>(name);
                            ReasonList.Add(data);

                            DBReasonLanguage dbdata = new DBReasonLanguage();
                            dbdata.code = data.code;
                            dbdata.name = data.name;
                            App.Database.SaveReason(dbdata);

                        }
                    }

                }
                else
                {
                    await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.ErrorLoadingData);
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
        public async void CheckLocation(string type)
        {
            try
            {
                lstmessage.Clear();
                if (!HttpRequest.CheckConnection())
                {
                    var objUser = App.Database.GetCheckinCheckoutLocation();
                    using (await MaterialDialog.Instance.LoadingDialogAsync(message: Resx.AppResources.pleaseWait))
                    {
                        //DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                        lstmessage = new List<clsMessages>();
                        if (objUser != null && objUser.Count > 0)
                        {
                            try
                            {
                                foreach (var item in objUser)
                                {
                                    var msg = new clsMessages();
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
                                                Location destinationCoordinates = new Location(item.lat, item.lng);
                                                double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                                                double distanceMeter = distance * 1000;
                                                Radius = distanceMeter;
                                                if (distanceMeter > item.radius)
                                                {
                                                    msg.Status = true;

                                                    //DependencyService.Get<IProgressBar>().Hide();
                                                    ////await App.Current.MainPage.DisplayAlert("AttendanceApp", Resx.AppResources.youAreInLocation, Resx.AppResources.ok);
                                                    //IsUserExist = true;
                                                    //IsAccordianOpen = !IsAccordianOpen;
                                                }
                                                else
                                                {
                                                    //DependencyService.Get<IProgressBar>().Hide();
                                                    //await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.youAreOutOfLocation);
                                                    //IsUserExist = false;
                                                    msg.Status = false;
                                                    msg.Message = Resx.AppResources.youAreOutOfLocation;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg.Status = false;
                                            msg.Message = Resx.AppResources.pleaseEnableYourLocationService;
                                            //DependencyService.Get<IProgressBar>().Hide();
                                            //await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.pleaseEnableYourLocationService);
                                            return;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        msg.Status = false;
                                        msg.Message = ex.Message;
                                    }
                                    lstmessage.Add(msg);
                                }

                            }
                            catch (Exception ex)
                            {
                                IsUserExist = false;
                                //DependencyService.Get<IProgressBar>().Hide();
                                await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
                            }
                            finally
                            {
                                //DependencyService.Get<IProgressBar>().Hide();
                                if (lstmessage != null && lstmessage.Count > 0)
                                {
                                    if (lstmessage.Any(x => x.Status))
                                    {
                                        IsUserExist = true;
                                        MessagingCenter.Send<string>(type, "NotifyMsg");
                                    }
                                    else
                                    {
                                        foreach (var item in lstmessage)
                                        {
                                            await DependencyService.Get<IXSnack>().ShowMessageAsync(item.Message);
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            //DependencyService.Get<IProgressBar>().Hide();
                            await DependencyService.Get<IXSnack>().ShowMessageAsync("Please checkIn/checkOut atleast first in net connectivity.");
                        }
                    }
                   

                    return;

                }

                using (await MaterialDialog.Instance.LoadingDialogAsync(message: Resx.AppResources.pleaseWait))
                {
                    //DependencyService.Get<IProgressBar>().Show(Resx.AppResources.pleaseWait);
                    App.Database.ClearCheckinCheckoutDetails();
                    var menuItem = await CommonMethods.GetLocations();

                    if (menuItem.locationData != null)
                    {
                        foreach (var item in menuItem.locationData)
                        {
                            DBLocationData dbdata = new DBLocationData();
                            var msg = new clsMessages();
                            dbdata.lat = item.lat;
                            dbdata.lng = item.lng;
                            dbdata.radius = item.radius;
                            App.Database.SaveCheckinCheckoutLocation(dbdata);

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
                                        Location destinationCoordinates = new Location(item.lat, item.lng);
                                        double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                                        double distanceMeter = distance * 1000;
                                        Radius = distanceMeter;
                                        if (distanceMeter > item.radius)
                                        {
                                            //DependencyService.Get<IProgressBar>().Hide();
                                            //IsUserExist = true;
                                            //IsAccordianOpen = !IsAccordianOpen;

                                            msg.Status = true;
                                        }
                                        else
                                        {
                                            //DependencyService.Get<IProgressBar>().Hide();
                                            //await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.youAreOutOfLocation);
                                            //IsUserExist = false;
                                            msg.Status = false;
                                            msg.Message = Resx.AppResources.youAreOutOfLocation;
                                        }
                                    }
                                }
                                else
                                {
                                    msg.Status = false;
                                    msg.Message = Resx.AppResources.pleaseEnableYourLocationService;
                                    //DependencyService.Get<IProgressBar>().Hide();
                                    //await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.pleaseEnableYourLocationService);
                                    return;
                                }

                            }
                            catch (Exception ex)
                            {
                                msg.Status = false;
                                msg.Message = ex.Message;
                                //DependencyService.Get<IProgressBar>().Hide();
                                //IsUserExist = false;
                                //await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
                            }
                          
                            lstmessage.Add(msg);
                        }


                    }
                    else
                    {
                        IsUserExist = false;
                        //DependencyService.Get<IProgressBar>().Hide();
                        await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.ErrorLoadingData);
                    }
                }
               
            }
            catch (Exception ex)
            {
                IsUserExist = false;
                //DependencyService.Get<IProgressBar>().Hide();
                await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
            }
            finally
            {
                //DependencyService.Get<IProgressBar>().Hide();
                if (lstmessage!=null && lstmessage.Count>0)
                {
                    if (lstmessage.Any(x=>x.Status))
                    {
                        IsUserExist = true;
                        MessagingCenter.Send<string>(type, "NotifyMsg");
                    }
                    else
                    {
                        foreach (var item in lstmessage)
                        {
                            await DependencyService.Get<IXSnack>().ShowMessageAsync(item.Message);
                        }
                    }
                }
                
            }
        }
    }
}
