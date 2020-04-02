using System;
using AttendanceApp.Helpers;

namespace AttendanceApp.Models
{
    public class DashboardMenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string GridColor { get; set; }
        public double LblFontSize {
            get
            {
                return CommonMethods.GetFontSizeBasedOnScreenHeight();
            } }
    }
    public static class DashboardTilesText
    {
        public static string CheckInChckout { get { return Resx.AppResources.checkInCheckOut; } }
        public static string LeaveRequest { get { return Resx.AppResources.leaveRequest; } }
        public static string ReasonRequest { get { return Resx.AppResources.reasonRequest; } }
        public static string UnjustifiedViolations { get { return Resx.AppResources.unjustifiedViolations; } }
        public static string MyAttendance { get { return Resx.AppResources.myAttendance; } }
        public static string ApproveReasons { get { return Resx.AppResources.approveReasons; } }
        public static string ApproveLeaves { get { return Resx.AppResources.approveLeaves; } }
    }
    public static class DashboardTilesImages
    {
        public static string CheckInChckoutImage { get { return "checkinwhite.png"; } }
        public static string LeaveRequestImage { get { return "leaveswhite.png"; } }
        public static string ReasonRequestImage { get { return "reasonwhite.png"; } }
        public static string UnjustifiedViolationsImage { get { return "violationswhite.png"; } }
        public static string MyAttendanceImage { get { return "attendancewhite.png"; } }
        public static string ApproveReasonsImage { get { return "approvereasonwhite.png"; } }
        public static string ApproveLeavesImage { get { return "approveleavewhite.png"; } }
    }

    public enum DashboardItemType
    {
        CheckInChckout,
        LeaveRequest,
        ReasonRequest,
        UnjustifiedViolations,
        MyAttendance,
        ApproveReasons,
        ApproveLeaves
    }

    public static class GridColorUtil
    {
        public static readonly string AnnouncementsColor = "#57bb8a";
        public static readonly string LeaveRequestColor = "#ee6421";
        public static readonly string TrainingEventsColor = "#2c6bdd";
        public static readonly string ManualsColor = "#ffae00";
        public static readonly string MyAttendanceColor = "#eb3179";
        public static readonly string CheckInChckoutColor = "#733ead";
        public static readonly string StoreColor = "#8f3724";
        public static readonly string ApproveLeavesColor = "#249899";
        public static readonly string UnjustifiedViolationsColor = "#df0024";
        public static readonly string ApproveReasonsColor = "#0d4eaf";
        public static readonly string ReasonRequestColor = "#963c00";
        public static readonly string ActivityIndicatorColor = "#ff8056";
        public static readonly string DefaultGridBorderColor = "#D3D3D3";
        public static readonly string SupportColor = "#48a0dc";
        public static readonly string IdeaExchangeColor = "#48a0dc";
    }
}
