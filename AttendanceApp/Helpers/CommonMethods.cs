using System;
using System.Threading.Tasks;
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
                string url = ServiceConfigrations.BaseUrl + ServiceConfigrations.GetOrganizationProfile;

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
    }
}
