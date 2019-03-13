using TaskTracker.Models;
using TaskTracker.ViewModels.Page;
using Xamarin.Forms;

namespace TaskTracker.Views
{
    public partial class MainPage : ContentPage
    {
        internal MainPage(Board selectedBoard)
        {
            var vm = new MainPageViewModel();

            BindingContext = vm;

            InitializeComponent();
        }
    }
}
