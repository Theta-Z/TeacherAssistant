using System.Windows;
using TeacherAssistant.DB;
using TeacherAssistant.DB.Service;
using TeacherAssistant.Helpers;

namespace TeacherAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _LoginAttempts = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _LoginAttempts++;
            var md5 = MD5Helper.GetHash(txtPass.Password);
            Clipboard.SetText(md5);

            var userService = new UserService() { DB = new LocalDB() };
            var loggedIn = userService.Login(txtUser.Text, md5);

            if (loggedIn)
                MessageBox.Show("Yay");
            else
                MessageBox.Show("Incorrect");
        }
    }
}