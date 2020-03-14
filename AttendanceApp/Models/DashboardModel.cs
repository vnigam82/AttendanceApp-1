using System;
namespace AttendanceApp.Models
{
    public class DashboardMenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string GridColor { get; set; }
    }
    public static class DashboardTilesText
    {
        public static string CheckInChckout { get { return "Check IN/Check OUT"; } }
        public static string LeaveRequest { get { return "Leave Request"; } }
        public static string ReasonRequest { get { return "Reason Request"; } }
        public static string UnjustifiedViolations { get { return "Unjustified Violations"; } }
        public static string MyAttendance { get { return "My Attendance"; } }
        public static string ApproveReasons { get { return "Approve Reasons"; } }
        public static string ApproveLeaves { get { return "Approve Leaves"; } }
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
