using System.Windows;
using TaskTracker.Data;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestManager manager = new RestManager(new RestService("1","1"));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var list = manager.GetUsers();

        }
    }
}
