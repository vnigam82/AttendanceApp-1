using System;
namespace AttendanceApp.Models
{
    public class BookingModel
    {
        public DateTime DateTime { get; set; }
        public string Direction { get; set; }
        public string Location { get; set; }
        public int? ReasonCode { get; set; }
        public Int64? HappinessOption { get; set; }
    }
    
}
