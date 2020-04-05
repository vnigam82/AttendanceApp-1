using System;
using System.Collections.ObjectModel;
using AttendanceApp.CustomControls;
using AttendanceApp.Dependency;
using AttendanceApp.Helpers;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.ViewModels
{
    public class MyAttendanceViewModel : BaseViewModel
    {
        #region Local Variable
        private INavigation _navigation;
        ServiceConfigrations service = new ServiceConfigrations();
        private DateTime _maxdate = DateTime.Now.Date,_fromdate=DateTime.Now.Date,_todate = DateTime.Now.Date;
        private ObservableCollection<MyAttendanceModel> _attendancelist;
        private Command _searchcommand;
        private bool _isenablesearchbutton = true;
        private double _lablefontsize = 0, _gridheaderrowfontsize = 0;
        #endregion
        public MyAttendanceViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            LabelFontSize = CommonMethods.GetFontSizeBasedOnScreenHeight();
            GridHeaderRowFontSize = CommonMethods.GetFontSizeBasedOnScreenHeightForGridRow();
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
        public double GridHeaderRowFontSize
        {
            get { return _gridheaderrowfontsize; }
            set
            {

                _gridheaderrowfontsize = value;
                OnPropertyChanged(nameof(GridHeaderRowFontSize));
            }
        }
        private bool iISBusy;
        public Command SerachCommand
        {
            get
            {
                return _searchcommand ?? (_searchcommand = new Command(() =>
                {
                    //iISBusy = true;
                    GetAttendanceList();
                }));
            }
        }


        public ObservableCollection<MyAttendanceModel> AttendanceList
        {
            get { return _attendancelist; }
            set
            {

                _attendancelist = value;
                OnPropertyChanged(nameof(AttendanceList));
            }
        }

        public DateTime MaxDate
        {
            get { return _maxdate; }
            set
            {

                _maxdate = value;
                OnPropertyChanged(nameof(MaxDate));
            }
        }
        public bool IsEnableSearchButton
        {
            get { return _isenablesearchbutton; }
            set
            {

                _isenablesearchbutton = value;
                OnPropertyChanged(nameof(IsEnableSearchButton));
            }
        }
        public DateTime FromDate
        {
            get { return _fromdate; }
            set
            {

                _fromdate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }

        public DateTime ToDate
        {
            get { return _todate; }
            set
            {

                _todate = value;
                OnPropertyChanged(nameof(ToDate));
            }
        }


        public async void GetAttendanceList()
        {
            try
            {
               // IsEnableSearchButton = false;
                if (!HttpRequest.CheckConnection())
                {
                    await DependencyService.Get<IXSnack>().ShowMessageAsync(Resx.AppResources.pleaseCheckYourNetworkConnection);
                    return;
                }
                DependencyService.Get<IProgressBar>().Show("Please wait...");

                string attendancedata="from="+FromDate.Date.ToString("yyyy-MM-dd") + "&to=" + ToDate.Date.ToString("yyyy-MM-dd");
                var menuItem = await CommonMethods.GetAttendanceList(attendancedata);
                AttendanceList = new ObservableCollection<MyAttendanceModel>();
                if (menuItem != null)
                {
                    if (menuItem.Count > 0)
                    {
                        DependencyService.Get<IProgressBar>().Hide();
                        foreach (var item in menuItem)
                        {
                            var data = new MyAttendanceModel();
                            data.date = item.date;
                            data.isAbsent = item.isAbsent;
                            data.isHoliday = item.isHoliday;
                            data.isLeave = item.isLeave;
                            data.isWeekend = item.isWeekend;
                            data.timeIn = item.timeIn;
                            data.timeOut = item.timeOut;
                            data.totalHours = item.totalHours;
                            data.remarks = item.remarks;
                            AttendanceList.Add(data);
                        }
                    }
                    else
                    {
                        await DependencyService.Get<IXSnack>().ShowMessageAsync("No records");
                    }

                }
                else
                {
                    await DependencyService.Get<IXSnack>().ShowMessageAsync("Error Loading Data");
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IProgressBar>().Hide();
                await DependencyService.Get<IXSnack>().ShowMessageAsync(ex.Message);
            }
            finally
            {
               // IsEnableSearchButton = true;
                DependencyService.Get<IProgressBar>().Hide();
            }
        }
    }
}
