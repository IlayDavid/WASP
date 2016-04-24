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

namespace Client.GUI
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        ICL _cl;
        User user;
        public WelcomeWindow()
        {
            _cl = new TCL();
            if (_cl.isInitialize() == 0)
            {
                CreateAdmin cAdmin = new CreateAdmin(_cl);
                cAdmin.ShowDialog();
            }

            List<Forum> forums = new List<Forum>();//_cl.getAllForums();
            forums.Add(new Forum() { Name = "aaa", Description = "bbb" });
            dgForums.ItemsSource = forums;
            //todo: show forums
            InitializeComponent();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLoginSU_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(_cl);
            login.Title = "Login as SU";
            login.ShowDialog();
            user = login.getUser();
        }

        private void btnEnterForum_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
