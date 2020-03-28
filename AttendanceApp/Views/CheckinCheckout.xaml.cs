using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class CheckinCheckout : ContentPage
    {
        CheckinCheckoutViewModel _checkincheckoutViewmodel;
        public static RadioOption GetSelectedRadio { get; set; }
        public CheckinCheckout()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _checkincheckoutViewmodel = ServiceContainer.Resolve<CheckinCheckoutViewModel>();
            _checkincheckoutViewmodel.AddMenuItems();
            _checkincheckoutViewmodel.InitializeRadioButtonList();
            _checkincheckoutViewmodel.GetReasonList();
            BindingContext = _checkincheckoutViewmodel;
            //headerView.BindingContext = _checkincheckoutViewmodel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            safeInsets.Top = Device.RuntimePlatform == Device.Android ? 0 : 40;
            mainlayout.Padding = safeInsets;

        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RadioOption;

            if (item == null)
                return;

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
    }
}
