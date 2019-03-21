using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskTracker.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardListItem : ContentView
	{
		public BoardListItem ()
		{
			InitializeComponent ();
		}
	}
}