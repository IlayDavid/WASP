using Client.BusinessLogic;
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

namespace Client.GUI
{
    /// <summary>
    /// Interaction logic for AddForum.xaml
    /// </summary>
    public partial class AddForum : Window
    {
        private Forum _forum = null;
        private List<string> questions = null;
        public AddForum()
        {
            InitializeComponent();
            questions = new List<string>();
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

                    questions = new List<string> { txtNewQuestion1.Text, txtNewQuestion2.Text };

                    Policy policy = new Policy(deletePost, passwordPeriod, emailVerification, seniority, usersSameTime, 
                        questions.ToArray(), notificationSelecting());

                    _forum = Session.bl.createForum(txtForumName.Text, txtForumDesc.Text, int.Parse(txtAdminID.Text),
                        txtAdminUserName.Text, txtAdminName.Text, txtEmail.Text, passPass.Password, policy);

                    Session.bl.defineForumPolicy(policy);
                    _forum = Session.bl.getForum(_forum.id);
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

        public Forum getForum()
        {
            return _forum;
        }
    }
}
