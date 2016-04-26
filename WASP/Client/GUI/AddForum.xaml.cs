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
        IBL _bl;
        Forum _forum = null;
        SuperUser _su;
        public AddForum(IBL bl, SuperUser su)
        {
            InitializeComponent();
            _bl = bl;
            _su = su;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _forum = _bl.createForum(_su.id, txtForumName.Text, txtForumDesc.Text, txtAdminUserName.Text,
                    txtAdminName.Text, txtEmail.Text, passPass.Password, null);

                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public Forum getForum()
        {
            return _forum;
        }
    }
}
