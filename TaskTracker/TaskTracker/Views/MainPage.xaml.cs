﻿using System.Linq;
using Syncfusion.ListView.XForms;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionChangedEventArgs = Syncfusion.SfCarousel.XForms.SelectionChangedEventArgs;

namespace TaskTracker.Views
{
    public partial class MainPage : ContentPage
    {
        internal MainPage(BoardVM selectedBoard)
        {
            var vm = new MainPageViewModel(selectedBoard);

            vm.DisplayExceptionMessage = (exMessage) => DisplayAlert("Rest error", exMessage, "OK");

            BindingContext = vm;

            InitializeComponent();
        }

        private async void SfListView_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            var vm = BindingContext as MainPageViewModel;
            TaskVM task = e.ItemData as TaskVM;

            if (task == null)
                return;

            Carousel.SelectedIndex =
                vm.SelectedBoard.Base.Columns.FirstOrDefault(x => x.Id == task.Base.ColumnId).Position;


            if (e.Action == DragAction.Start)
            {
                vm.IsDeleteIconVisible = true;
            }

            if (e.Action == DragAction.Dragging)
            {
                //var position = new Point(e.Position.X - this.Carousel.Bounds.X, e.Position.Y - this.Carousel.Bounds.Y);

                //if (this.Header.Bounds.Contains(position))
                //    this.DeleteHeader.BackgroundColor = Color.Red;
                //else
                //    this.DeleteHeader.BackgroundColor = Color.OrangeRed;
            }

            if (e.Action == DragAction.Drop)
            {
                //    var position = new Point(e.Position.X - this.Carousel.Bounds.X, e.Position.Y - this.Carousel.Bounds.Y);

                //    if (this.DeleteHeader.Bounds.Contains(position))
                //    {
                //        await System.Threading.Tasks.Task.Delay(100);

                //        vm.SelectedBoard.ColumnsCollection[Carousel.SelectedIndex].TaskCollection.Remove(e.ItemData as TaskVM);
                //    }

                vm.IsDeleteIconVisible = false;
            }
        }

        private void SfListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}
