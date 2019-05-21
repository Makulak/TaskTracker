using System;
using System.IO;
using System.Runtime.CompilerServices;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.VM
{
    internal class UserVM : BaseVM
    {
        public User Base { get; private set; }

        #region ModelProperties

        public int Id => Base.Id;

        public string Login {
            get => Base.Login;
            set {
                Base.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password {
            get => Base.Password;
            set {
                Base.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Mail {
            get => Base.Mail;
            set {
                Base.Mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }

        public int[] BoardIds {
            get => Base.BoardIds;
            set {
                Base.BoardIds = value;
                OnPropertyChanged(nameof(BoardIds));
            }
        }

        public string ImageUrl => Base.ImageUrl;

        #endregion

        #region ViewModelProperties

        public ImageSource Image { get; private set; }

        #endregion

        public UserVM()
        {
            Base = new User();
        }

        public static implicit operator UserVM(User user)
        {
            UserVM newUser = new UserVM();
            newUser.Base = user;

            if (string.IsNullOrEmpty(newUser.ImageUrl))
            {
                newUser.Image = ImageSource.FromFile("user.xml");
            }
            else
            {
                newUser.Image = ImageSource.FromUri(new Uri(newUser.ImageUrl));
            }

            return newUser;
        }
    }
}