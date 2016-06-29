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

                    foreach (string q in Session.forum.policy.questions)
                    {
                        ListViewItem item = makeQuestionItem(q);
                        lstQuestions.Items.Add(item);
                    }                
                }
                else
                    gBoxRestore.IsEnabled = false;

                if (!(Session.forum.policy.notification == Policy.NOTIFICATION.selective))
                    gridInteractivity.IsEnabled = false;
            }
            catch { }
        }

        private ListViewItem makeQuestionItem(string q)
        {
            StackPanel sp = new StackPanel();

            sp.Children.Add(new Label { Content = q });
            sp.Children.Add(new Label { Content = "Answer: " });
            TextBox txtAns = new TextBox();

            sp.Children.Add(txtAns);

            ListViewItem item = new ListViewItem() { Content = sp, DataContext = txtAns};
            return item;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
          //  try
            {
                List<string> answers = new List<string>();

                //take answers, if it defined in the policy.
                if (Session.forum.policy.questions != null && Session.forum.policy.questions.Length != 0)
                    foreach (ListBoxItem item in lstQuestions.Items)
                    {
                        string ans = ((TextBox)item.DataContext).Text;
                        answers.Add(ans);
                    }

                bool online = Session.forum.policy.notification == Policy.NOTIFICATION.online ? true : false;
                if (Session.forum.policy.notification == Policy.NOTIFICATION.selective)
                {
                    online = rdbOn.IsChecked.Value ? true : false;
                }

                User user = Session.bl.subscribeToForum(int.Parse(txtID.Text), txtUsername.Text, txtName.Text,
                    txtmail.Text, passPassword.Password, Session.forum.id, answers, online);

                Session.user = Session.bl.login(user.userName, passPassword.Password, Session.forum.id, "");
                Session.AddNewNotifications();
                MessageBox.Show("Your client-session is: " + Session.user.client_session, "", MessageBoxButton.OK, MessageBoxImage.Information);

                if (Session.forum.policy.emailVerification)
                {
                    MessageBox.Show("An email with verification code has been sent!");
                    VerifyEmail emailVerify = new VerifyEmail();
                    emailVerify.ShowDialog();
                }
                this.Close();
            }
           // catch (Exception ee)
            {
               // MessageBox.Show(ee.Message);
            }
        }

    }
}
