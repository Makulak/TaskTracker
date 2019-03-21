using System;
using TaskTracker.Resources;
using TaskTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TaskTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var cultureInfo = DependencyService.Get<ILocalization>().GetCurrentCultureInfo();

                TaskTracker.Resources.AppResources.Culture = cultureInfo;
                DependencyService.Get<ILocalization>().SetLocale(cultureInfo);
            }

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
