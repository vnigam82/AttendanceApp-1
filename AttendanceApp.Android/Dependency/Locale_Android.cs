using System;
using Xamarin.Forms;
using System.Threading;
using AttendanceApp.Droid.Dependency;
using AttendanceApp.Dependency;

[assembly: Dependency(typeof(Locale_Android))]
namespace AttendanceApp.Droid.Dependency
{
    public class Locale_Android : ILocale
    {
        public void SetLocale()
        {

            var androidLocale = Java.Util.Locale.Default; // user's preferred locale
            App.countryCode = androidLocale.Country;
            var netLocale = androidLocale.ToString().Replace("_", "-");
            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        /// <remarks>
        /// Not sure if we can cache this info rather than querying every time
        /// </remarks>
        public string GetCurrent()
        {
            var androidLocale = Java.Util.Locale.Default; // user's preferred locale

            // en, es, ja
            var netLanguage = androidLocale.Language.Replace("_", "-");
            // en-US, es-ES, ja-JP
            var netLocale = androidLocale.ToString().Replace("_", "-");

            try
            {

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

                //string[] tokens = netLocale.Split('-');

                //if (tokens[0] == "ar")
                //{
                //    netLanguage = "ar-AE";
                //    netLocale = "ar-AE";
                //}
                //else
                //{
                //    netLanguage = "en-US";
                //    netLocale = "en-US";
                //}





            }
            catch (Exception ex)
            {
            }







            #region Debugging output
            Console.WriteLine("android:  " + androidLocale.ToString());
            Console.WriteLine("netlang:  " + netLanguage);
            Console.WriteLine("netlocale:" + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            return netLocale;
        }
    }
}