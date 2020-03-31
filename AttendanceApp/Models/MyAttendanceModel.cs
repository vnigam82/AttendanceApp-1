using System;
using AttendanceApp.Utils;

namespace AttendanceApp.Models
{
    public class MyAttendanceModel : HttpRequestResponseStatus
    {
        public string date { get; set; }
        public bool isAbsent { get; set; }
        public bool isHoliday { get; set; }
        public bool isLeave { get; set; }
        public bool isWeekend { get; set; }
        public string timeIn { get; set; }
        public string timeOut { get; set; }
        public string totalHours { get; set; }
        public string remarks { get; set; }



        public string FinalTimeIn {
            get
            {
                if (isAbsent || isHoliday || isLeave || isWeekend || string.IsNullOrEmpty(timeIn))
                {
                    return "--";
                }
                else
                {
                    string time = string.Empty;
                    if (!string.IsNullOrWhiteSpace(timeIn))
                    {
                        TimeSpan ts = TimeSpan.Parse(timeIn);
                        DateTime dt = DateTime.Today.Add(ts);
                        time = dt.ToString("hh:mm tt");
                    }
                    return time;
                }
            }
        }

        public string FinalTimeOut
        {
            get
            {
                if (isAbsent || isHoliday || isLeave || isWeekend || string.IsNullOrEmpty(timeOut))
                {
                    return "--";
                }
                else
                {
                    string time = string.Empty;
                    if (!string.IsNullOrWhiteSpace(timeOut))
                    {
                        TimeSpan ts = TimeSpan.Parse(timeOut);
                        DateTime dt = DateTime.Today.Add(ts);
                        time = dt.ToString("hh:mm tt");
                    }
                    return time;
                }
            }
        }
        public string FinalTotalHours
        {
            get
            {
                if (isAbsent || isHoliday || isLeave || isWeekend || string.IsNullOrEmpty(totalHours))
                {
                    return "--";
                }
                else
                {
                    string time = string.Empty;
                    if (!string.IsNullOrWhiteSpace(totalHours))
                    {
                        TimeSpan ts = TimeSpan.Parse(totalHours);
                        DateTime dt = DateTime.Today.Add(ts);
                        time = dt.ToString("hh:mm tt");
                    }
                    return time;
                }
            }
        }

        public string FinalRemarks
        {
            get
            {
                if (string.IsNullOrEmpty(remarks) && isAbsent)
                {
                    return "Absent";
                }
                else if(string.IsNullOrEmpty(remarks) && isLeave)
                {
                    return "Leave";
                }
                else if (string.IsNullOrEmpty(remarks) && isWeekend)
                {
                    return "Weekend";
                }
                else if (string.IsNullOrEmpty(remarks) && isHoliday)
                {
                    return "Holiday";
                }
                else
                {
                    return remarks;
                }
            }
        }

        public string GridColor
        {
            get
            {
                if (isAbsent)
                {
                    return "#FF9999";
                }
                else if (isLeave)
                {
                    return "#b3ff99";
                }
                else if (isWeekend)
                {
                    return "#ffdf80";
                }
                else if (isHoliday)
                {
                    return "#99bbff";
                }
                else
                {
                    return "#ffffff";
                }
            }
        }
    }
}
