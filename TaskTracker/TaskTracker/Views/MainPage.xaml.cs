using System.Collections.Generic;
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

            _viewModel.DisplayAddColumn = SetAddColumnPopup;
            _viewModel.DisplayExceptionMessage = (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            _viewModel.DisplayAddTask = SetNewTaskPopup;
            _viewModel.DisplayRenameColumnPopup = SetRenameColumnPopup;
            _viewModel.DisplayTaskPage = (task) => Navigation.PushAsync(new TaskPage(task));
            _viewModel.DisplayMoveTaskPopup = SetColumnChoosePopup;
            _viewModel.CloseMoveTaskPopup = () => MainPopup.IsOpen = false;

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

        private void SetColumnChoosePopup()
        {
            MainPopup.PopupView.AcceptCommand = _viewModel.MoveTaskCommand;
            MainPopup.PopupView.HeaderTitle = AppResources.MoveTask;
            MainPopup.PopupView.ContentTemplate = Application.Current.Resources["MoveTaskPopup"] as DataTemplate;
            MainPopup.PopupView.ShowFooter = false;
            MainPopup.PopupView.HeightRequest = 350;

            MainPopup.Show();
        }

        private void LvTasks_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Start)
            {
            }

            //    if (e.Action == DragAction.Dragging)
            //    {
            //        var position = new Point(e.Position.X - this.lvTasks.Bounds.X - this.lvTasks.Bounds.X, e.Position.Y - this.lvTasks.Bounds.Y - this.lvTasks.ItemSize);

            //        if (Header.Bounds.Contains(position))
            //        {
            //            DeleteImage.IsVisible = true;
            //        }
            //        else
            //        {
            //            DeleteImage.IsVisible = false;
            //        }
            //    }

            if (e.Action == DragAction.Drop)
            {
                //var position = new Point(e.Position.X - this.lvTasks.Bounds.X - this.lvTasks.Bounds.X, e.Position.Y - this.lvTasks.Bounds.Y - this.lvTasks.ItemSize);

                ColumnVM column = _viewModel.SelectedBoard.ColumnsCollection[_viewModel.SelectedColumnPosition];
                TaskVM task = e.ItemData as TaskVM;

                if (column == null || task == null)
                    return;

                //if (Header.Bounds.Contains(position))
                //{
                //    column.RemoveTask(task);
                //}
                //else
                //{
                _viewModel.MoveTask(task.Id, column.Id, e.NewIndex);
                //}

                //DeleteImage.IsVisible = false;
            }
        }
    }
}