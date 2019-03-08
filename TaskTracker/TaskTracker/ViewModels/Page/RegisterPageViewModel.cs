using System;
using System.Windows.Input;
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
                OnPropertyChanged("Mail");
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

        public Action DisplayMainPage;
        public Action DisplayInvalidPasswordMessage;

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(OnRegister);
        }

        private void OnRegister()
        {
            bool registerOk = true;

            if (PasswordOne != PasswordTwo)
            {
                DisplayInvalidPasswordMessage();
            }
            else if (registerOk)
            {
                DisplayMainPage();
            }
        }
    }
}