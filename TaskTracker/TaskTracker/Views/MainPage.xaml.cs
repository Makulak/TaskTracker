using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        internal MainPage(BoardVM board)
        {
            InitializeComponent();

            MainPageViewModel vm = new MainPageViewModel(board);

            BindingContext = vm;
        }
    }
}