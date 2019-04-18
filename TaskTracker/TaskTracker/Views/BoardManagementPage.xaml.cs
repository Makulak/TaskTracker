using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Resources;
using TaskTracker.ViewModels.Page;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SelectionChangedEventArgs = Syncfusion.XForms.ComboBox.SelectionChangedEventArgs;

namespace TaskTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BoardManagementPage : ContentPage
    {
        private BoardManagementPageViewModel _viewModel;

		internal BoardManagementPage (BoardVM selectedBoard)
        {
            _viewModel = new BoardManagementPageViewModel(selectedBoard);

            _viewModel.ShowBoardPage = () =>  Navigation.PopAsync();
            _viewModel.ShowConfirmDeletePopup = SetConfirmDeletePopup;
            _viewModel.DisplayExceptionMessage += (exMessage) => DisplayAlert(AppResources.Error, exMessage, AppResources.Ok);

            BindingContext = _viewModel;
			InitializeComponent ();
		}

        private void SetConfirmDeletePopup()
        {
            //BoardManagementPopup.Show();
        }

        private void userSelectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.AssignUserToBoard(e.Value as UserVM);
            userSelectionComboBox.IsDropDownOpen = false;
        }
    }
}