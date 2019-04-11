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

        private async void SfListView_OnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (e.ItemData is TaskVM task)
                await Navigation.PushAsync(new TaskPage(task));
        }

        private void SfListView_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            TaskVM task = e.ItemData as TaskVM;

            ColumnVM bindingContext = BindingContext as ColumnVM;

            if (task == null || bindingContext == null)
                return;

            if (e.Action == DragAction.Start)
            {
                DeleteImage.IsVisible = true;
            }

            if (e.Action == DragAction.Dragging)
            {
                var position = new Point(e.Position.X - ListView.Bounds.X, e.Position.Y - ListView.Bounds.Y);

                if (Header.Bounds.Contains(position))
                    Header.BackgroundColor = Color.FromHex("#EF9A9A");
                else
                    Header.BackgroundColor = Color.Transparent;
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - ListView.Bounds.X, e.Position.Y - ListView.Bounds.Y);

                if (Header.Bounds.Contains(position))
                {
                    bindingContext.RemoveTask(task);
                    bindingContext.Base.Tasks.Remove(task.Base);
                }
                else
                {
                    bindingContext.MoveTask(task.Id, e.NewIndex);
                }

                Header.BackgroundColor = Color.Transparent;
                DeleteImage.IsVisible = false;
            }
        }

        private void CarouselItem_OnBindingContextChanged(object sender, EventArgs e)
        {
            ColumnVM column = BindingContext as ColumnVM;

            if (column == null)
                return;

            column.DisplayAddTask = () => AddNewTaskPopup.Show();
            column.DisplayExceptionMessage = (exMessage) => Application.Current.MainPage.DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);
        }
    }
}