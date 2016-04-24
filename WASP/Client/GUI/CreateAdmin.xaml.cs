using Client.CommunicationLayer;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateAdmin : Window
    {
        private ICL _cl;
        private SuperUser _su;
        public CreateAdmin(ICL cl)
        {
            _cl = cl;
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string name = txtName.Text;
            string mail = txtmail.Text;
            string pass = txtPassword.Text;
            int id = int.Parse(txtID.Text);

            _su = _cl.initialize(name, userName, id, mail, pass);
            if (_su != null)
                MessageBox.Show("Your registration is complete!\nyou can log in now as an admin.");
            else
                MessageBox.Show("Your registration could not be completed.\n Admin has been already registered.");
            this.Close();
        }

        public SuperUser getAdmin()
        {
            return _su;
        }

        private void chkShowChar_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkShowChar_UnChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
