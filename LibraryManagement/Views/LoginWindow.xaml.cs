using LibraryManagement.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AppDbContext db = new AppDbContext();

            var user = db.Users.FirstOrDefault(x =>
                x.Login == LoginBox.Text &&
                x.Password == PasswordBox.Password);

            if (user != null)
            {
                MainWindow window = new MainWindow();

                window.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }
    }
}