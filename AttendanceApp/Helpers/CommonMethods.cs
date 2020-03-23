﻿using System;
using System.Threading.Tasks;
using AttendanceApp.Database;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.Helpers
{
    public static class CommonMethods
    {
        
        public static async Task<OrganizationProfile> GetOrganizationProfile()
        {
            OrganizationProfile obj = new OrganizationProfile();
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetOrganizationProfile;

                var userinfo = await HttpRequest.GetRequest(url);

                var serviceResult = JsonConvert.DeserializeObject<OrganizationProfile>(userinfo.Result);
                if (serviceResult != null)
                {
                    obj = serviceResult;
                    return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static async Task<clsUserProfile> GetUserProfile()
        {
            clsUserProfile obj = new clsUserProfile();
            LoginDBModel objUser = App.Database.GetLoggedInUser();
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetUserProfile+objUser.UserGUID+"/Profile";

                var userinfo = await HttpRequest.GetRequest(url);
                if (userinfo.Status)
                {
                    var serviceResult = JsonConvert.DeserializeObject<clsUserProfile>(userinfo.Result);
                    if (serviceResult != null)
                    {
                        obj = serviceResult;
                        obj.Status = userinfo.Status;
                        obj.Message = userinfo.Message;
                        obj.Result = userinfo.Result;
                    }
                    else
                    {
                        obj.Message = userinfo.Message;
                        obj.Status = userinfo.Status;
                        obj.StatusCode = userinfo.StatusCode;
                    }
                }
                else
                {
                    obj.Message = userinfo.Message;
                    obj.StatusCode = userinfo.StatusCode;
                }
                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static async Task<HttpRequestResponseStatus> LogInToUser(string jsonData)
        {
            HttpRequestResponseStatus obj = new HttpRequestResponseStatus();
            try
            {
                var userinfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl1, ServiceConfigrations.Login + "?IsDeviceValidated=false", jsonData);


                if (userinfo.Status)
                {
                    string serviceResult = JsonConvert.DeserializeObject<string>(userinfo.Result);
                    if (serviceResult != null)
                    {
                        obj.Result = serviceResult;
                        obj.Status = userinfo.Status;
                        obj.Message = userinfo.Message;

                        return obj;
                    }
                    else
                    {
                        return obj;
                    }
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

    }
}
