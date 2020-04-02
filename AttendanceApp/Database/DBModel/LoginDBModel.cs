﻿using System;
using SQLite;

namespace AttendanceApp.Database
{
    public class BaseDbModel
    {
       
    }
    public class LoginDBModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGUID { get; set; }
        public bool RememberMe { get; set; }
    }

    public class OrgProfileDBModel 
    {
        public string name { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
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
        }
        public string type { get; set; }
    }

    
    public class clsDBUserProfile 
    {
        public string email { get; set; }
        public string fullName { get; set; }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
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
        }
        public string type { get; set; }
    }
}
