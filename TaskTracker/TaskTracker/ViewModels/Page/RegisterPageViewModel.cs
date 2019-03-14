using System;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class RegisterPageViewModel : BaseViewModel
    {
        public string Mail
        {
            get => _mail;
            set
            {
                _mail = value;
                OnPropertyChanged("Login");
            }
        }
        private string _mail;

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

        public string PasswordOne
        {
            get => _passwordOne;
            set
            {
                _passwordOne = value;
                OnPropertyChanged("PasswordOne");
            }
        }
        private string _passwordOne;

        public string PasswordTwo
        {
            get => _passwordTwo;
            set
            {
                _passwordTwo = value;
                OnPropertyChanged("PasswordTwo");
            }
        }
        private string _passwordTwo;

        public ICommand RegisterCommand { get; set; }

        public Action DisplayInvalidPasswordMessage;
        public Action DisplayLoginPage;

        private readonly RestManager _manager;

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(OnRegister);

            _manager = new RestManager(new RestService());
        }

        private async void OnRegister()
        {
            if (PasswordOne != PasswordTwo)
            {
                DisplayInvalidPasswordMessage?.Invoke();
            }
            else 
            {
                try
                {
                    await _manager.Register(new User(Login, PasswordOne, Mail));
                    DisplayLoginPage?.Invoke();
                }
                catch (RestException ex)
                {
                    DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
                }
            }
        }
    }
}