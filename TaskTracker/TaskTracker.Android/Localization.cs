using System.Globalization;
using System.Threading;
using TaskTracker.Droid;
using TaskTracker.Resources;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localization))]
namespace TaskTracker.Droid
{
    class Localization : ILocalization
    {
        public void SetLocale(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");

            try
            {
                return new System.Globalization.CultureInfo("PL-pl"); //TODO: Poprawić na poprawny język
                //return new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                return new System.Globalization.CultureInfo("en");
            }
        }
    }
}