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
		public CarouselItem ()
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
            
        }
    }
}