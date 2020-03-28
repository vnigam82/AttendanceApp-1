using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Android;

namespace AttendanceApp.Droid
{
    [Activity(Label = "AttendanceApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string permissionAF = Manifest.Permission.AccessFineLocation;
        const string permissionAC = Manifest.Permission.AccessCoarseLocation;
        const int RequestLocationId = 0;
        readonly string[] permissions =
    {
         Manifest.Permission.AccessFineLocation,
         Manifest.Permission.AccessCoarseLocation,
    };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);


            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(permissionAF) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }
            if ((int)Build.VERSION.SdkInt >= 23 && CheckSelfPermission(permissionAC) != (int)Permission.Granted)
            {
                RequestPermissions(permissions, RequestLocationId);
            }

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}