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
        public CarouselItem()
        {
            InitializeComponent();
        }

        private void CarouselItem_OnBindingContextChanged(object sender, EventArgs e)
        {
            ColumnVM column = BindingContext as ColumnVM;

            if (column == null)
                return;

            column.DisplayAddTask = () => AddNewTaskPopup.Show();
            column.DisplayExceptionMessage = (exMessage) => Application.Current.MainPage.DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
            column.DisplayTaskPage = (task) => Navigation.PushAsync(new TaskPage(task));
        }

        private void LvTasks_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            if (e.Action == DragAction.Start)
            {
            }

            if (e.Action == DragAction.Dragging)
            {
                var position = new Point(e.Position.X - this.lvTasks.Bounds.X - this.lvTasks.Bounds.X, e.Position.Y - this.lvTasks.Bounds.Y - this.lvTasks.ItemSize);

                if (Header.Bounds.Contains(position))
                {
                    DeleteImage.IsVisible = true;
                }
                else
                {
                    DeleteImage.IsVisible = false;
                }
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - this.lvTasks.Bounds.X - this.lvTasks.Bounds.X, e.Position.Y - this.lvTasks.Bounds.Y - this.lvTasks.ItemSize);

                ColumnVM column = BindingContext as ColumnVM;
                TaskVM task = e.ItemData as TaskVM;

                if (column == null || task == null)
                    return;

                if (Header.Bounds.Contains(position))
                {
                    column.RemoveTask(task);
                }
                else
                {
                    column.MoveTask(task.Id, e.NewIndex);
                }

                DeleteImage.IsVisible = false;
            }
        }
    }
}