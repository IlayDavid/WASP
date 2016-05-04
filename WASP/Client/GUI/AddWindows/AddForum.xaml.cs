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
        Forum _forum = null;
        public AddForum()
        {
            InitializeComponent();
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
                    Policy policy = new Policy(deletePost, passwordPeriod, emailVerification, seniority, usersSameTime);
                    _forum = Session.bl.createForum(Session.user.id, txtForumName.Text, txtForumDesc.Text, int.Parse(txtAdminID.Text),
                    txtAdminUserName.Text, txtAdminName.Text, txtEmail.Text, passPass.Password, policy);
                }
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
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

        private void chkbOwner_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
