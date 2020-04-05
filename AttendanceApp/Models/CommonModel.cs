using System;
using System.Collections.Generic;
using AttendanceApp.Utils;
using Newtonsoft.Json;

namespace AttendanceApp.Models
{
    public class Logo
    {
        public string src { get; set; }
        public string ImageSource { get
            {
                string imagesource = string.Empty;
                if(string.IsNullOrWhiteSpace(src))
                {
                    imagesource = "profile.png";
                }
                else
                {
                    imagesource = src;
                }
                return imagesource;
            } }

        public string type { get; set; }
    }

    public class OrganizationProfile
    {
        public string name { get; set; }
        public Logo logo { get; set; }
    }

    public class Language
    {
        public string en { get; set; }
        public string ar { get; set; }
        public string de { get; set; }
        public string fr { get; set; }
    }

    public class ReasonLanguage
    {
        
        public string En { get; set; }
        
        public string ar { get; set; }
        public string de { get; set; }
        public string fr { get; set; }
    }
    public class clsUserProfile: HttpRequestResponseStatus
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public Logo picture { get; set; }
    }

    public class BaseModel
    {
        public int email { get; set; }
        public string fullName { get; set; }
    }


    public class LocationData
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public double radius { get; set; }
    }
    public class ClsLocationData: HttpRequestResponseStatus
    {
        public string name { get; set; }
        public string description { get; set; }
        public string mapLocation { get; set; }
        public List<LocationData> locationData { get; set; }
    }

    public class clsReasons
    {
        public int code { get; set; }
        public string name { get; set; }
        public ReasonLanguage langData { get; set; }
    }

    public class clsMessages
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
