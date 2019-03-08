using System;
using System.Windows.Input;
using TaskTracker.Data;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class LoginPageViewModel : BaseViewModel
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

        public Action DisplayInvalidLoginMessage;
        public Action DisplayRegisterPage;
        public Action DisplayForgetPasswordPage;
        public Action DisplayMainPage;

        private RestManager _manager;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgetPasswordCommand = new Command(OnForgetPasswordCommand);
        }

        private void OnForgetPasswordCommand()
        {
            throw new NotImplementedException();
        }

        private void OnRegister()
        {
            throw new NotImplementedException();
        }

        private void OnLogin()
        {
            _manager = new RestManager(new RestService(Mail,Password));

            bool isLoginOk = true;

            if (isLoginOk)
            {
                DisplayMainPage();
            }
            else
            {
                DisplayInvalidLoginMessage();
            }
        }
    }
}
