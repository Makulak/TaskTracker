using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
            RegisterPageViewModel vm = new RegisterPageViewModel();

            vm.DisplayInvalidPasswordMessage += () => DisplayAlert(string.Empty, AppResources.InvalidPassword, AppResources.Ok);
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            vm.DisplayLoginPage += () => Navigation.PopAsync();

            BindingContext = vm;

            InitializeComponent ();
		}
	}
}