using System;
using System.Threading.Tasks;
using Syncfusion.SfRadialMenu.XForms;
using TaskTracker.Helpers;
using TaskTracker.Resources;
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
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            vm.DisplayAddBoard += () => AddNewBoardPopup.IsOpen = true;

            BindingContext = vm;

            BoardListBehavior behavior = new BoardListBehavior();
            behavior.DeleteItemCommand = vm.DeleteBoardCommand;
            behavior.EditItemCommand = vm.EditBoardCommand;

            InitializeComponent();

            LvBoard.Behaviors.Insert(0, behavior);
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