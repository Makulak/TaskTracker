using Syncfusion.ListView.XForms;
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

            _viewModel = new MainPageViewModel(board);

            _viewModel.DisplayAddColumn += SetAddColumnPopup;
            _viewModel.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            _viewModel.DisplayAddTask = SetNewTaskPopup;
            _viewModel.DisplayRenameColumnPopup = SetRenameColumnPopup;
            _viewModel.DisplayTaskPage = (task) => Navigation.PushAsync(new TaskPage(task));

            BindingContext = _viewModel;
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

        private void SetNewTaskPopup()
        {
            MainPopup.PopupView.AcceptCommand = _viewModel.AddTaskCommand;
            MainPopup.PopupView.HeaderTitle = AppResources.AddNewTask;
            MainPopup.PopupView.ContentTemplate = Application.Current.Resources["AddTaskPopup"] as DataTemplate;

            MainPopup.Show();
        }

        private void SetRenameColumnPopup()
        {
            MainPopup.PopupView.AcceptCommand = _viewModel.RenameColumnCommand;
            MainPopup.PopupView.HeaderTitle = AppResources.RenameColumn;
            MainPopup.PopupView.ContentTemplate = Application.Current.Resources["RenameColumnPopup"] as DataTemplate;

            MainPopup.Show();
        }

        private void LvTasks_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
        }
    }
}