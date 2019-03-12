using System;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
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
        public ICommand ForgetPasswordCommand { get; set; }

        public Action DisplayRegisterPage;
        public Action DisplayForgetPasswordPage;
        public Action DisplayMainPage;
        public Action<string> DisplayExceptionMessage;

        private readonly RestManager _manager;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgetPasswordCommand = new Command(OnForgetPasswordCommand);

            _manager = new RestManager(new RestService());
        }

        private void OnForgetPasswordCommand()
        {
            DisplayForgetPasswordPage();
        }

        private void OnRegister()
        {
            DisplayRegisterPage();
        }

        private async void OnLogin()
        {
            try
            {
                await _manager.LogIn(new User(Login, Password));
                DisplayMainPage();
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }
    }
}
