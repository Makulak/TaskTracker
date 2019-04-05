using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.ListView.XForms;
using TaskTracker.Helpers;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Xamarin.Forms.ItemTappedEventArgs;

namespace TaskTracker.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselItem : ContentView
    {
        //public static readonly BindableProperty MainPageVmProperty =
        //    BindableProperty.Create(nameof(MainPageVm), typeof(MainPageViewModel), typeof(CarouselItem));

        //public MainPageViewModel MainPageVm {
        //    get => (MainPageViewModel)GetValue(MainPageVmProperty);
        //    set => SetValue(MainPageVmProperty, value);
        //}

        public CarouselItem()
        {
            InitializeComponent();
        }

        private void SfListView_OnItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private void SfListView_OnItemDragging(object sender, ItemDraggingEventArgs e)
        {
            TaskVM task = e.ItemData as TaskVM;

            ColumnVM bindingContext = BindingContext as ColumnVM;

            if (task == null || bindingContext == null)
                return;

            if (e.Action == DragAction.Start)
            {
                //int pos = MainPageVm.SelectedBoard.Base.Columns.FirstOrDefault(x => x.Id == task.Base.ColumnId).Position;

                //if (MainPageVm.CarouselSelectedIndex != pos)
                //    MainPageVm.CarouselSelectedIndex = pos;

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
                }

                Header.BackgroundColor = Color.Transparent;
                DeleteImage.IsVisible = false;
            }
        }
    }
}