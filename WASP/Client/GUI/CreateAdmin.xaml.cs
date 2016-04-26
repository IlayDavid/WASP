using Client.BusinessLogic;
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
        private IBL _bl;
        private SuperUser _su;
        private bool createFinish = false;
        public CreateAdmin(IBL bl)
        {
            InitializeComponent();
            _bl = bl;
            _su = null;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string name = txtName.Text;
            string mail = txtmail.Text;
            string pass = passPassword.Password;
            int id = 0;
            try
            {
                id = int.Parse(txtID.Text);
            }
            catch
            {
                MessageBox.Show("ERROR: id is illegal"); return;
            }
            try
            {
                _su = _bl.initialize(name, userName, id, mail, pass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); return;
            }
            if (_su != null)
                MessageBox.Show("Your registration is complete!\nyou can log in now as an admin.");
            else
                MessageBox.Show("Your registration could not be completed.\nAdmin has been already registered.");
            createFinish = true;
            this.Close();
        }

        public SuperUser getAdmin()
        {
            return _su;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !createFinish;
        }
    }
}
