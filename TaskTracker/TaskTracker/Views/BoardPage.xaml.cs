using TaskTracker.ViewModels.Page;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardPage : ContentPage
	{
		public BoardPage ()
		{
            BoardPageViewModel vm = new BoardPageViewModel();

            BindingContext = vm;

			InitializeComponent ();
		}
	}
}