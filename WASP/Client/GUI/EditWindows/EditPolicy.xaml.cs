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

namespace Client.GUI.EditWindows
{
    /// <summary>
    /// Interaction logic for EditPolicy.xaml
    /// </summary>
    public partial class EditPolicy : Window
    {
        private List<string> questions = null;
        public EditPolicy()
        {
            InitializeComponent();
            resetChanges();
        }

        private void resetChanges()
        {
            txtModSen.Text = Session.forum.policy.seniority.ToString();
            txtPassPeriod.Text = Session.forum.policy.passwordPeriod.ToString();
            txtUserSameTime.Text = Session.forum.policy.usersSameTime.ToString();

            chkbAdmin.IsChecked = Session.forum.policy.isAdminCanDeletePost();
            chkbOwner.IsChecked = Session.forum.policy.isOwnerCanDeletePost();
            chkbModerator.IsChecked = Session.forum.policy.isModeratorCanDeletePost();
            chkbAdmin.IsChecked = Session.forum.policy.emailVerification;

            questions = Session.forum.policy.questions.ToList();
            if (questions == null)
                questions = new List<string>();
            foreach (string s in questions)
            {
                lstBoxRestoreQuestion.Items.Add(new ListBoxItem() { Content = s });
            }

            if (Session.forum.policy.notification == Policy.NOTIFICATION.online)
                rdbOn.IsChecked = true;
            else if (Session.forum.policy.notification == Policy.NOTIFICATION.offline)
                rdbOff.IsChecked = true;
            else
                rdbSelective.IsChecked = true;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Session.user is SuperUser)
                {
                    int deletePost = deletePostPermission();
                    int passwordPeriod = int.Parse(txtPassPeriod.Text);
                    bool emailVerification = chkbEmailVer.IsChecked.Value;
                    int seniority = int.Parse(txtModSen.Text);
                    int usersSameTime = int.Parse(txtUserSameTime.Text);

                    Policy policy = new Policy(deletePost, passwordPeriod, emailVerification, seniority, usersSameTime,
                        questions.ToArray(), notificationSelecting());
                    Session.bl.defineForumPolicy(policy);
                }
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private Policy.NOTIFICATION notificationSelecting()
        {
            if (rdbOff.IsChecked.Value)
                return Policy.NOTIFICATION.offline;
            else if (rdbOn.IsChecked.Value)
                return Policy.NOTIFICATION.online;
            else
                return Policy.NOTIFICATION.selective;
        }

        private int deletePostPermission()
        {
            int ret = 0;
            ret += chkbAdmin.IsChecked.Value ? Policy.admin : 0;
            ret += chkbAdmin.IsChecked.Value ? Policy.moderator : 0;
            ret += chkbAdmin.IsChecked.Value ? Policy.owner : 0;
            return ret;
        }

        private void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string newQ = txtNewQuestion.Text;
            if (newQ.Equals(""))
            {
                MessageBox.Show("Enter a question.");
                return;
            }
            questions.Add(newQ);
            ListBoxItem item = new ListBoxItem() { Content = newQ };
            lstBoxRestoreQuestion.Items.Add(item);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            resetChanges();
        }
    }
}
