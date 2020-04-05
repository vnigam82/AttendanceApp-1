using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApp.CustomControls.RadioButton;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Utils;
using AttendanceApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XF.Material.Forms.UI.Dialogs;


namespace AttendanceApp.Views
{
    public partial class CheckinCheckout : ContentPage,IDisposable
    {
        CheckinCheckoutViewModel _checkincheckoutViewmodel;
        public static RadioOption GetSelectedRadio { get; set; }
        public CheckinCheckout()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);

            //headerView.BindingContext = _checkincheckoutViewmodel;

            MessagingCenter.Subscribe<string>("AttendanceApp", "NotifyMsg", (msg) =>
            {
                if (msg == "In")
                {
                    ac2.IsOpen = !ac2.IsOpen;
                    _checkincheckoutViewmodel.Direction = "In";
                }
                else
                {
                    ac2.IsOpen = !ac2.IsOpen;
                    _checkincheckoutViewmodel.Direction = "Out";
                }

            });

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            safeInsets.Top = Device.RuntimePlatform == Device.Android ? 0 : 40;
            mainlayout.Padding = safeInsets;

            _checkincheckoutViewmodel = ServiceContainer.Resolve<CheckinCheckoutViewModel>();
            _checkincheckoutViewmodel.AddMenuItems();
            _checkincheckoutViewmodel.InitializeRadioButtonList();
            _checkincheckoutViewmodel.GetReasonList();
            BindingContext = _checkincheckoutViewmodel;



          

        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RadioOption;

            if (item == null)
                return;

            _checkincheckoutViewmodel.SelectedHappinessOption = item;
            foreach (var s in _checkincheckoutViewmodel.RadioOptionsList.Where(x => x.IsSelected))
            {
                s.IsSelected = false;
            }

            item.IsSelected = true;
            GetSelectedRadio = item;
        }


        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lstFilterType.SelectedItem = null;
        }

        void materialCheckin_Clicked(System.Object sender, System.EventArgs e)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime utc = DateTime.UtcNow;
            DateTime UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, localZone);

            if (sender == materialCheckin)
            {
                _checkincheckoutViewmodel.CheckLocation("In");
               
                _checkincheckoutViewmodel.GPSDateTime = UAE;
            }
            else
            {
                _checkincheckoutViewmodel.CheckLocation("Out");
                _checkincheckoutViewmodel.GPSDateTime = UAE;
            }
        }

        void btnClose_Clicked(System.Object sender, System.EventArgs e)
        {
            ac2.IsOpen = !ac2.IsOpen;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BindingContext = null;
            GC.Collect();
        }

        public void Dispose()
        {
            BindingContext = null;
            GC.Collect();
        }
        //void btnSubmit_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    //_checkincheckoutViewmodel.ExecuteSubmitCommand();

        //        if (_checkincheckoutViewmodel.IsUserExist)
        //        {
        //            ac2.IsOpen = !ac2.IsOpen;
        //        }

        //}
    }
}
