using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AttendanceApp.Dependency;
using AttendanceApp.Droid.Dependency;

[assembly: Xamarin.Forms.Dependency(typeof(Mock))]
namespace AttendanceApp.Droid.Dependency
{
    public class Mock : IMockLocation
    {
        public Boolean IsMockLocation(Xamarin.Essentials.Location location) //Copying ESSENTIAL VARIABLE TO ANDRIOD LOCATION VARIABLE
        {
            Boolean isMock = false;
            Context context = Android.App.Application.Context;
            Location alocation = new Location("");
            alocation.Latitude = location.Latitude;
            alocation.Longitude = location.Longitude;
            //alocation.Accuracy = (float)location.Accuracy;
            //alocation.Altitude = (float)location.Altitude;
            //alocation.Speed = (float)location.Speed;
            //alocation.Time = location.Timestamp.Ticks;
           // if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBeanMr2)
                isMock = alocation.IsFromMockProvider;  //using marshmallow so this part is executing
          //  else
               // isMock =Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AllowMockLocation).Equals("0"); //this is below jellybean
            return isMock;
        }
    }

}