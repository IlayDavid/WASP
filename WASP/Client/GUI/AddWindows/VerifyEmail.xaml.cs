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
    /// Interaction logic for VerifyEmail.xaml
    /// </summary>
    public partial class VerifyEmail : Window
    {
        public VerifyEmail()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Session.bl.confirmEmail(Session.user.id, Session.forum.id, int.Parse(txtCode.Text));
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
