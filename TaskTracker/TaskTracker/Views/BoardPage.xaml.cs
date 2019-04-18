using System;
using System.Threading.Tasks;
using TaskTracker.Helpers;
using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardPage : ContentPage
    {
        private BoardPageViewModel _viewModel;

        public BoardPage ()
		{
            BoardPageViewModel vm = new BoardPageViewModel();

            vm.DisplayMainPage = (selectedBoard) => Navigation.PushAsync(new MainPage(selectedBoard));
            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            vm.DisplayAddBoardPopup += SetAddNewBoardPopup;
            vm.DisplayEditBoardPage = (selectedBoard) => Navigation.PushAsync(new BoardManagementPage(selectedBoard));
            vm.DisplayDeleteBoardPopup += SetDeleteBoardPopup;

            BindingContext = vm;
            _viewModel = vm;

            BoardListBehavior behavior = new BoardListBehavior();
            behavior.DeleteItemCommand = vm.DeleteBoardButtonCommand;
            behavior.EditItemCommand = vm.EditBoardButtonCommand;

            InitializeComponent();

            LvBoard.Behaviors.Insert(0, behavior);
		}

        private void SetAddNewBoardPopup()
        {
            BoardPopup.PopupView.AcceptCommand = _viewModel.AddNewBoardCommand;
            BoardPopup.PopupView.HeaderTitle = AppResources.AddNewBoard;
            BoardPopup.PopupView.AcceptButtonText = AppResources.Accept;
            BoardPopup.PopupView.DeclineButtonText = AppResources.Cancel;
            BoardPopup.PopupView.ContentTemplate = Application.Current.Resources["AddBoardPopup"] as DataTemplate;

            BoardPopup.Show();
        }

        private void SetEditBoardPopup()
        {
            BoardPopup.PopupView.AcceptCommand = _viewModel.EditBoardCommand;
            BoardPopup.PopupView.HeaderTitle = AppResources.EditBoard;
            BoardPopup.PopupView.AcceptButtonText = AppResources.Accept;
            BoardPopup.PopupView.DeclineButtonText = AppResources.Cancel;
            BoardPopup.PopupView.ContentTemplate = Application.Current.Resources["EditBoardPopup"] as DataTemplate;

            BoardPopup.Show();
        }

        private void SetDeleteBoardPopup()
        {
            BoardPopup.PopupView.AcceptCommand = _viewModel.DeleteBoardCommand;
            BoardPopup.PopupView.HeaderTitle = AppResources.DeleteBoard;
            BoardPopup.PopupView.AcceptButtonText = AppResources.Yes;
            BoardPopup.PopupView.DeclineButtonText = AppResources.No;
            BoardPopup.PopupView.ContentTemplate = Application.Current.Resources["DeleteBoardPopup"] as DataTemplate;

            BoardPopup.Show();
        }

        private async void SfPullToRefresh_OnRefreshing(object sender, EventArgs e)
        {
            PullToRefresh.IsRefreshing = true;

            var vm = BindingContext as BoardPageViewModel;

            vm?.GetUserBoards();

            await Task.Delay(1500);

            PullToRefresh.IsRefreshing = false;
        }

        private void BoardPage_OnAppearing(object sender, EventArgs e)
        {
            _viewModel.GetUserBoards();
        }
    }
}