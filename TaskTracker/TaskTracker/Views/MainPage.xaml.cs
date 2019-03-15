using TaskTracker.Models;
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

            BindingContext = vm;

            InitializeComponent();
        }
    }
}
