using Client.DataClasses;
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

namespace Client.GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class AddMember : Window
    {
        public AddMember()
        {
            InitializeComponent();
            try
            {
                if (Session.forum.policy.questions != null)
                {
                    string question = Session.forum.policy.questions[0];
                    lblUserQusetion.Content = question;                    
                }
                else
                    gBoxRestore.IsEnabled = false;
            }
            catch { }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = Session.bl.subscribeToForum(int.Parse(txtID.Text), txtUsername.Text, txtName.Text,
                    txtmail.Text, passPassword.Password, Session.forum.id);
                Session.user = Session.bl.login(user.userName, user.password, Session.forum.id);
                if(Session.forum.policy.emailVerification)
                {
                    MessageBox.Show("Check your email for verification code");
                    VerifyEmail emailVerify = new VerifyEmail();
                    emailVerify.ShowDialog();
                }
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

    }
}
