using System.Globalization;

namespace TaskTracker.Resources
{
    public interface ILocalization
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo info);
    }
}