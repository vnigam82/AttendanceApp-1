using System;
using SQLite;

namespace AttendanceApp.Database
{
    public class BaseDbModel
    {
       
    }
    public class LoginDBModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGUID { get; set; }
        public bool RememberMe { get; set; }
    }

    public class OrgProfileDBModel 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string src { get; set; }
     
        public string type { get; set; }
    }

    
    public class clsDBUserProfile 
    {
       
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public string src { get; set; }
        public string ImageSource
        {
            get
            {
                string imagesource = string.Empty;
                if (string.IsNullOrWhiteSpace(src))
                {
                    imagesource = "profile.png";
                }
                else
                {
                    imagesource = src;
                }
                return imagesource;
            }
            set { }
        }
        public string type { get; set; }
    }

    public class DBLocationData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double radius { get; set; }
    }

    public class DBReasonLanguage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string reasonDirection { get; set; }

    }
    public class DBBookingModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string Direction { get; set; }
        public string Location { get; set; }
        public int? ReasonCode { get; set; }
        public Int64? HappinessOption { get; set; }
    }
}
