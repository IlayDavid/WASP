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

            if (Session.forum.policy.questions == null || Session.forum.policy.questions.Count() == 0)
            {
                string[] questions = Session.forum.policy.questions;
                lblQuestion1.Content = questions[0];
                lblQuestion2.Content = questions[1];
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            List<string> answers = new List<string> { txtAnswer1.Text, txtAnswer2.Text };
            try
            {
                int user_id = int.Parse(txtUserName.Text);
                string newpassword = txtNewpassword.Text;
                if (/*answers[0].Equals("") ||*/ newpassword.Equals(""))
                {
                    MessageBox.Show("Please enter answer and new password");
                    return;
                }
                Session.bl.restorePasswordbyAnswers(user_id, Session.forum.id, answers, newpassword);
                MessageBox.Show("Your new password is: " + newpassword, "Password restored!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch { }
        }
    }
}
