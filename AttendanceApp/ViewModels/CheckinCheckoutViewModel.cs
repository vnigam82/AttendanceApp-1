using System;
using System.Collections.ObjectModel;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using Xamarin.Forms;

namespace AttendanceApp.ViewModels
{
    public class CheckinCheckoutViewModel:BaseViewModel
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
    }
}
