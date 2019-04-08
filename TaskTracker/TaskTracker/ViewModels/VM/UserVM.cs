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

        public string Login
        {
            get => Base.Login;
            set
            {
                Base.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => Base.Password;
            set
            {
                Base.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Mail
        {
            get => Base.Mail;
            set
            {
                Base.Mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }

        public int[] BoardIds
        {
            get => Base.BoardIds;
            set
            {
                Base.BoardIds = value;
                OnPropertyChanged(nameof(BoardIds));
            }
        }

        public string ImageUrl => Base.ImageUrl;

        #endregion

        #region ViewModelProperties

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        private ImageSource _image;

        #endregion

        public UserVM()
        {
            Base = new User();
            Image = ImageSource.FromFile("user.xml");
        }

        public static implicit operator UserVM(User user)
        {
            return new UserVM()
            {
                Base = user
            };
        }
    }
}
