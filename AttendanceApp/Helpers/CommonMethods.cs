using System;
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
        static LoginDBModel objUser = App.Database.GetLoggedInUser();
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
            try
            {
                string url = ServiceConfigrations.BaseUrl1 + ServiceConfigrations.GetUserProfile+objUser.UserGUID+"/Profile";

                var userinfo = await HttpRequest.GetRequest(url);

                var serviceResult = JsonConvert.DeserializeObject<clsUserProfile>(userinfo.Result);
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
    }
}
