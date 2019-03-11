using System.CodeDom;
using System.Windows;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestManager manager = new RestManager(new RestService());

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var list = manager.LogIn(new User() { Login = "admin", Password = "tracker123" });
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            var list = manager.GetUsers();
        }
    }
}
