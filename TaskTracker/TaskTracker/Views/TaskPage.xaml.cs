using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.PopupLayout;
using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPage : ContentPage
    {
        private TaskPageViewModel _viewModel;
        internal TaskPage(TaskVM selectedTask)
        {
            _viewModel = new TaskPageViewModel(selectedTask);
            _viewModel.ShowUserListPopup = SetUserListPopup;

            _viewModel.ClosePage += () => Navigation.PopAsync();
            _viewModel.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            _viewModel.CloseUserListPopup += () => TaskPopup.IsOpen = false;

            BindingContext = _viewModel;
            InitializeComponent();
        }

        private void SetUserListPopup()
        {
            TaskPopup.PopupView.HeaderTitle = AppResources.AssignUser;
            TaskPopup.PopupView.AppearanceMode = AppearanceMode.OneButton;
            TaskPopup.PopupView.ContentTemplate = Application.Current.Resources["AssignUserPopup"] as DataTemplate;

            TaskPopup.Show();
        }
    }
}