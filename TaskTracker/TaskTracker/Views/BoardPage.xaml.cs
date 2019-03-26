using System;
using System.Threading.Tasks;
using Syncfusion.SfRadialMenu.XForms;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardPage : ContentPage
	{
		public BoardPage ()
		{
            BoardPageViewModel vm = new BoardPageViewModel();
            vm.DisplayMainPage = (selectedBoard) => Navigation.PushAsync(new MainPage(selectedBoard));
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert("Rest error", exMessage, "OK");
            vm.HideKeyboard += () => { }; //TODO: Zrobić ukrywanie
            vm.DisplayAddBoard += () => AddNewBoardPopup.Show();

            BindingContext = vm;

			InitializeComponent ();
		}

        private async void SfPullToRefresh_OnRefreshing(object sender, EventArgs e)
        {
            PullToRefresh.IsRefreshing = true;

            var vm = BindingContext as BoardPageViewModel;

            vm?.GetUserBoards();

            await Task.Delay(1500);

            PullToRefresh.IsRefreshing = false;
        }

        private void SfListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as BoardPageViewModel;
            vm?.DisplayMainPage(e.ItemData as BoardVM);
        }
    }
}