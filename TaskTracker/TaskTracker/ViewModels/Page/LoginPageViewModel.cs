using System;
using System.IO;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class LoginPageViewModel : BaseViewModel
    {
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }
        private string _login;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private string _password;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public Action DisplayRegisterPage;
        public Action DisplayMainPage;

        private readonly RestManager _manager;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);

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

                await _manager.LogIn(new User(Login, Password));
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
