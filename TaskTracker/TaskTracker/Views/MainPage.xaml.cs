using System.Linq;
using Syncfusion.ListView.XForms;
using TaskTracker.Models;
using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionChangedEventArgs = Syncfusion.SfCarousel.XForms.SelectionChangedEventArgs;

namespace TaskTracker.Views
{
    public partial class MainPage : ContentPage
    {
        internal MainPage(BoardVM selectedBoard)
        {
            var vm = new MainPageViewModel(selectedBoard);

            vm.DisplayExceptionMessage = (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);

            BindingContext = vm;

            InitializeComponent();
        }
    }
}
