using System;
using Foundation;
using System.Threading;
using Xamarin.Forms;
using AttendanceApp.iOS.Dependency;
using AttendanceApp.Dependency;

[assembly: Dependency(typeof(Locale_iOS))]
namespace AttendanceApp.iOS.Dependency
{
    public class Locale_iOS : ILocale
    {
        public void SetLocale()
        {
            try
            {

                var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
                var netLocale = iosLocaleAuto.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;


            }
            catch (Exception ex)
            {
            }
        }


        public string GetCurrent()
        {

            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;

            var netLocale = NSLocale.CurrentLocale.CollatorIdentifier;
            var netLanguage = NSLocale.CurrentLocale.CollatorIdentifier;

            // var netLocale = iosLocaleAuto.Replace("_", "-");
            // var netLanguage = iosLanguageAuto.Replace("_", "-");

            var sqlLiteResult = App.Database.GetLanguage();
            if (sqlLiteResult != null)
            {
                netLanguage = sqlLiteResult.LangKey;
                netLocale = sqlLiteResult.LangKey;
            }
            else
            {
                netLanguage = "en-US";
                netLocale = "en-US";
            }
            //if (netLocale == "ar-US")
            //{ 
            //    netLanguage = "ar-AE";
            //    netLocale = "ar-AE";
            //}
            //else
            //{ netLanguage = "en-US";
            //  netLocale = "en-US"; 

            //}




            return netLanguage;


        }

    }
}