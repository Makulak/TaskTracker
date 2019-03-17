using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskPage : ContentPage
	{
		internal TaskPage (TaskVM selectedTask)
        {
            BindingContext = new TaskPageViewModel(selectedTask);
			InitializeComponent ();
		}
	}
}