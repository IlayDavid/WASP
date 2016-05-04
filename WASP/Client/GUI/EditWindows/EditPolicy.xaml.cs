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
        public EditPolicy()
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
                    Session.bl.defineForumPolicy(Session.user.id, Session.forum.id, policy);
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
    }
}
