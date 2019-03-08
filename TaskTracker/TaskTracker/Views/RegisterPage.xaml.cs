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

            vm.DisplayMainPage += () => Navigation.PushAsync(new MainPage());
            vm.DisplayInvalidPasswordMessage += () =>
                DisplayAlert("Zjebałeś XD", "W dupe se pogrzeb tą rejestracją", "Ok, wypierdalam");
			InitializeComponent ();
		}
	}
}