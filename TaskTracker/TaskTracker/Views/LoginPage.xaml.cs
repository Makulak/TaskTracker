using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            LoginPageViewModel vm = new LoginPageViewModel();

            vm.DisplayMainPage += () => Application.Current.MainPage = new NavigationPage(new BoardPage());
            vm.DisplayRegisterPage += async () => await Navigation.PushAsync(new RegisterPage());
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);

            BindingContext = vm;
            InitializeComponent();
        }
    }
}