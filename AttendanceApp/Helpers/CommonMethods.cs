using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceApp.Database;
using AttendanceApp.Models;
using AttendanceApp.ServiceConfigration;
using AttendanceApp.Utils;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace AttendanceApp.Helpers
{
    public static class CommonMethods
    {
        public static async Task ShowPopup(string messgae)
        {
            await App.Current.MainPage.DisplayAlert("AttendanceApp", messgae, "OK");
        }

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
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetUserProfile + objUser.UserGUID + "/Profile";

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

        public static async Task<ClsLocationData> GetLocations()
        {
            ClsLocationData obj = new ClsLocationData();
            LoginDBModel objUser = App.Database.GetLoggedInUser();
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetAssignedLocations + objUser.UserGUID + "/AssignedLocations";

                var userinfo = await HttpRequest.GetRequest(url);
                if (userinfo.Status)
                {
                    var serviceResult = JsonConvert.DeserializeObject<List<ClsLocationData>>(userinfo.Result);
                    if (serviceResult.Count>0)
                    {
                        LocationData locationData = new LocationData();
                        foreach (var item in serviceResult)
                        {
                            var serviceData = JsonConvert.DeserializeObject<LocationData>(item.mapLocation);
                            
                            locationData.lat = serviceData.lat;
                            locationData.lng = serviceData.lng;
                            locationData.radius = serviceData.radius;
                            break;
                        }

                        obj.locationData = locationData;
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
                        obj.Result = serviceResult;
                        obj.Status = userinfo.Status;
                        obj.Message = userinfo.Message;

                        return obj;
                    }
                }
                else
                {
                    
                    obj.Status = userinfo.Status;
                    obj.Message = userinfo.Message;

                    return obj;
                }
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static async Task<List<clsReasons>> GetReasons()
        {
            List<clsReasons> obj = new List<clsReasons>();
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetReasons;

                var userinfo = await HttpRequest.GetRequest(url);

                var serviceResult = JsonConvert.DeserializeObject<List<clsReasons>>(userinfo.Result);
                if (serviceResult != null)
                {
                    if (serviceResult.Count>0)
                    {
                        obj = serviceResult;
                    }
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

        public static async Task<HttpRequestResponseStatus> BookingAttendance(string jsonData)
        {
            LoginDBModel objUser = App.Database.GetLoggedInUser();
            HttpRequestResponseStatus obj = new HttpRequestResponseStatus();
            try
            {
                //string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetAssignedLocations + objUser.UserGUID + "/Attendance";
                var userinfo = await HttpRequest.PostRequest(ServiceConfigrations.BaseUrl1, ServiceConfigrations.GetAssignedLocations + objUser.UserGUID + "/Attendance", jsonData);


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
                        obj.Result = serviceResult;
                        obj.Status = userinfo.Status;
                        obj.Message = userinfo.Message;
                        return obj;
                    }
                }
                else
                {
                    
                    obj.Status = userinfo.Status;
                    obj.Message = userinfo.Message;

                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.Message = ex.Message;
                return obj;
            }
        }
        public static async Task<List<MyAttendanceModel>> GetAttendanceList(string data)
        {
            List<MyAttendanceModel> obj = new List<MyAttendanceModel>();
            LoginDBModel objUser = App.Database.GetLoggedInUser();
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetUserProfile + objUser.UserGUID + "/Attendance?" + data;

                var userinfo = await HttpRequest.GetRequest(url);
                var serviceResult = JsonConvert.DeserializeObject<List<MyAttendanceModel>>(userinfo.Result);
                if (serviceResult != null)
                {
                    if (serviceResult.Count > 0)
                    {
                        obj = serviceResult;
                    }
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

        public static double GetFontSizeBasedOnScreenHeight()
        {
            double fontSize = 0;
            double resolution = 160;
            int[] ptSizes = { 4, 6, 8, 10, 12 };

            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            // Rotation (0, 90, 180, 270)
            var rotation = mainDisplayInfo.Rotation;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;

            // Screen density
            var density = mainDisplayInfo.Density;

            NamedSize[] namedSizes =
            {
                NamedSize.Default, NamedSize.Micro, NamedSize.Small,
                NamedSize.Medium, NamedSize.Large
            };

            foreach (double ptSize in ptSizes)
            {
                fontSize = xamarinWidth * density / 72;
            }
            return fontSize;
        }
        public static double GetFontSizeBasedOnScreenHeightForGridRow()
        {
            double fontSize = 0;
            double resolution = 160;
            int[] ptSizes = { 4, 6, 8, 10, 12 };

            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            // Rotation (0, 90, 180, 270)
            var rotation = mainDisplayInfo.Rotation;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;

            // Screen density
            var density = mainDisplayInfo.Density;

            NamedSize[] namedSizes =
            {
                NamedSize.Default, NamedSize.Micro, NamedSize.Small,
                NamedSize.Medium, NamedSize.Large
            };

            foreach (double ptSize in ptSizes)
            {
                fontSize = xamarinWidth * density / 90;
            }
            return fontSize;
        }
    }
}
