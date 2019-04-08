using System;
using System.IO;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class LoginPageViewModel : BaseViewModel
    {
        public UserVM UserData
        {
            get => _userData;
            set
            {
                _userData = value;
                OnPropertyChanged(nameof(UserData));
            }
        }
        private UserVM _userData;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public Action DisplayRegisterPage;
        public Action DisplayMainPage;

        private readonly RestManager _manager;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);

            UserData = new User();

            _manager = new RestManager(new RestService());
        }

        private void OnRegister()
        {
            DisplayRegisterPage?.Invoke();
        }

        private async void OnLogin()
        {
            try
            {
                ShowWaitForm = true;

                await _manager.LogIn(UserData.Base);
                DisplayMainPage?.Invoke();
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
            finally
            {
                ShowWaitForm = false;
            }
        }
    }
}
