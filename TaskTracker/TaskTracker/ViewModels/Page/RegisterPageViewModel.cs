using System;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class RegisterPageViewModel : BaseViewModel
    {
        public UserVM NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }
        private UserVM _newUser;

        public string PasswordOne
        {
            get => _passwordOne;
            set
            {
                _passwordOne = value;
                OnPropertyChanged(nameof(PasswordOne));
            }
        }
        private string _passwordOne;

        public string PasswordTwo
        {
            get => _passwordTwo;
            set
            {
                _passwordTwo = value;
                OnPropertyChanged(nameof(PasswordTwo));
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

            NewUser = new UserVM();

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
                    ShowWaitForm = true;

                    NewUser.Password = PasswordOne;

                    await _manager.Register(NewUser.Base);
                    DisplayLoginPage?.Invoke();
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
}