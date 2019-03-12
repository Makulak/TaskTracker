using TaskTracker.ViewModels.Page;
using Xamarin.Forms;

namespace TaskTracker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var vm = new MainPageViewModel();

            BindingContext = vm;

            InitializeComponent();
        }
    }
}
