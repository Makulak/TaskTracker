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

        public bool ShowWaitForm
        {
            get => _showWaitForm;
            set
            {
                _showWaitForm = value;
                if (value)
                    ControlsOpacity = 0.25f;
                else
                    ControlsOpacity = 1;

                OnPropertyChanged("ShowWaitForm");
            }
        }
        private bool _showWaitForm;

        public float ControlsOpacity
        {
            get => _controlsOpacity;
            set
            {
                _controlsOpacity = value;
                OnPropertyChanged("ControlsOpacity");
            }
        }
        private float _controlsOpacity = 1;


        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgetPasswordCommand { get; set; }

        public Action DisplayRegisterPage;
        public Action DisplayForgetPasswordPage;
        public Action DisplayMainPage;

        private readonly RestManager _manager;

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgetPasswordCommand = new Command(OnForgetPassword);

            _manager = new RestManager(new RestService());
        }

        private void OnForgetPassword()
        {
            DisplayForgetPasswordPage?.Invoke();
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
