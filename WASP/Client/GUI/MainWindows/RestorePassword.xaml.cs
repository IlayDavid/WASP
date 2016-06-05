using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.GUI.MainWindows
{
    /// <summary>
    /// Interaction logic for RestorePassword.xaml
    /// </summary>
    public partial class RestorePassword : Window
    {
        public RestorePassword()
        {
            InitializeComponent();

            txtAnswer.Visibility = Visibility.Hidden;
            txtNewpassword.Visibility = Visibility.Hidden;
            lblans.Visibility = Visibility.Hidden;
            lblPsw.Visibility = Visibility.Hidden;
            btnRestore.Visibility = Visibility.Hidden;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            string answer = txtAnswer.Text;
            string username = txtUserName.Text;
            string newpassword = txtNewpassword.Text;
            if (answer.Equals("") || newpassword.Equals(""))
            {
                MessageBox.Show("Please enter answer and new password");
                return;
            }
            string password = Session.bl.restorePasswordbyAnswer(username, answer, newpassword);
            if (password != null)
                MessageBox.Show("Your new password is: "+password, "Password restored!", MessageBoxButton.OK, MessageBoxImage.Information);
                
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            if (username.Equals(""))
            {
                MessageBox.Show("Please enter your username");
                return;
            }
            string question = Session.bl.getUserQuestion(username);
            if(question == null)
            {
                MessageBox.Show("You may type wrong username or you did not define question.");
                return;
            }
            lblQuestion.Content = question;
            txtUserName.IsEnabled = false;

            txtAnswer.Visibility = Visibility.Visible;
            txtNewpassword.Visibility = Visibility.Visible;
            lblans.Visibility = Visibility.Visible;
            lblPsw.Visibility = Visibility.Visible;
            btnRestore.Visibility = Visibility.Visible;
        }
    }
}
