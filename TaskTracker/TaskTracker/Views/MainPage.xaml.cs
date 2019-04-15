using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        internal MainPage(BoardVM board)
        {
            InitializeComponent();

            MainPageViewModel vm = new MainPageViewModel(board);

            vm.DisplayAddColumn += SetAddColumnPopup;
            vm.DisplayAddTask += SetAddTaskPopup;

            vm.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);

            BindingContext = vm;
            _viewModel = vm;
        }

        private void SetAddColumnPopup()
        {
            MainPopup.PopupView.AcceptCommand = _viewModel.AddNewColumnCommand;
            MainPopup.PopupView.HeaderTitle = AppResources.AddNewColumn;
            MainPopup.PopupView.AcceptButtonText = AppResources.Accept;
            MainPopup.PopupView.DeclineButtonText = AppResources.Cancel;
            MainPopup.PopupView.ContentTemplate = Application.Current.Resources["AddColumnPopup"] as DataTemplate;

            MainPopup.Show();
        }

        private void SetAddTaskPopup()
        {
            MainPopup.PopupView.AcceptCommand = _viewModel.AddNewTaskCommand;
            MainPopup.PopupView.HeaderTitle = AppResources.AddNewTask;
            MainPopup.PopupView.AcceptButtonText = AppResources.Accept;
            MainPopup.PopupView.DeclineButtonText = AppResources.Cancel;
            MainPopup.PopupView.ContentTemplate = Application.Current.Resources["AddTaskPopup"] as DataTemplate;

            MainPopup.Show();
        }
    }
}