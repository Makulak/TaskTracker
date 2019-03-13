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
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert("Rest error", exMessage, "OK");
            vm.DisplayForgetPasswordPage += () => DisplayAlert("Wypierdalaj", "Jeszcze tego nie zrobilem", "bez spiny");

            BindingContext = vm;
            InitializeComponent();
        }
    }
}