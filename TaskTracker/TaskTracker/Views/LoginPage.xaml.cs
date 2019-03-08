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
            vm.DisplayInvalidLoginMessage += () =>
                DisplayAlert("Error kurła", "Spierdalaj, zły login albo hasło", "Spoczko");

            vm.DisplayMainPage += () => Navigation.PushAsync(new MainPage());
            vm.DisplayRegisterPage += () => Navigation.PushAsync(new RegisterPage());

            BindingContext = vm;
            InitializeComponent();
        }
    }
}