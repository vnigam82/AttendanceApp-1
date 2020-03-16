using System;
using System.Collections.ObjectModel;
using AttendanceApp.Models;
using Xamarin.Forms;

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
                    //if (result.Id == 3)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("twoWheeler");
                    //}
                    //if (result.Id == 4)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("fourWheelerParking");
                    //}
                    //if (result.Id == 5)
                    //{
                    //    Xamarin.Forms.Shell.Current.GoToAsync("foodservice");
                    //}
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
    }
}
