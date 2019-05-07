using System;
using Syncfusion.ListView.XForms;
using TaskTracker.Resources;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselItem : ContentView
    {
        private ColumnVM _viewModel;

        public CarouselItem()
        {
            InitializeComponent();
        }

        private void CarouselItem_OnBindingContextChanged(object sender, EventArgs e)
        {
            _viewModel = BindingContext as ColumnVM;

            if (_viewModel == null)
                return;

            
            _viewModel.DisplayExceptionMessage = (exMessage) => Application.Current.MainPage.DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            
        }

        //private void LvTasks_OnItemDragging(object sender, ItemDraggingEventArgs e)
        //{
        //    if (e.Action == DragAction.Start)
        //    {
        //    }

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

        //    if (e.Action == DragAction.Drop)
        //    {
        //        var position = new Point(e.Position.X - this.lvTasks.Bounds.X - this.lvTasks.Bounds.X, e.Position.Y - this.lvTasks.Bounds.Y - this.lvTasks.ItemSize);

        //        ColumnVM column = BindingContext as ColumnVM;
        //        TaskVM task = e.ItemData as TaskVM;

        //        if (column == null || task == null)
        //            return;

        //        if (Header.Bounds.Contains(position))
        //        {
        //            column.RemoveTask(task);
        //        }
        //        else
        //        {
        //            column.MoveTask(task.Id, e.NewIndex);
        //        }

        //        DeleteImage.IsVisible = false;
        //    }
        //}

        //private void SetNewTaskPopup()
        //{
        //    CarouselItemPopup.PopupView.AcceptCommand = _viewModel.AddTaskCommand;
        //    CarouselItemPopup.PopupView.HeaderTitle = AppResources.AddNewTask;
        //    CarouselItemPopup.PopupView.ContentTemplate = Application.Current.Resources["AddTaskPopup"] as DataTemplate;

        //    CarouselItemPopup.Show();
        //}

        //private void SetRenameColumnPopup()
        //{
        //    CarouselItemPopup.PopupView.AcceptCommand = _viewModel.RenameColumnCommand;
        //    CarouselItemPopup.PopupView.HeaderTitle = AppResources.RenameColumn;
        //    CarouselItemPopup.PopupView.ContentTemplate = Application.Current.Resources["RenameColumnPopup"] as DataTemplate;

        //    CarouselItemPopup.Show();
        //}
    }
}