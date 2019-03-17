using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.Views
{
    public partial class MainPage : ContentPage
    {
        internal MainPage(BoardVM selectedBoard)
        {
            var vm = new MainPageViewModel(selectedBoard);
            vm.DisplayExceptionMessage = () => DisplayAlert("Rest error", exMessage, "OK");

            BindingContext = vm;

            InitializeComponent();
        }
    }
}
