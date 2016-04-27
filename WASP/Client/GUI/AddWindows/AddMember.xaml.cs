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
        User _user = null;
        public AddMember()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _user = Session.bl.subscribeToForum(int.Parse(txtID.Text), txtUsername.Text, txtName.Text,
                    txtmail.Text, passPassword.Password, Session.forum.ID);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public User getUser() { return _user; }
    }
}
