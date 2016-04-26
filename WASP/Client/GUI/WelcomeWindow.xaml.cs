using Client.CommunicationLayer;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<Forum> forums;
        public WelcomeWindow()
        {
            InitializeComponent();
            _cl = new TCL();
            if (_cl.isInitialize() == 0)
            {
                CreateAdmin cAdmin = new CreateAdmin(_cl);
                cAdmin.ShowDialog();
            }
            forums = _cl.getAllForums();
            var forumsView = ForumView.getView(forums);
            forumsView.Add(new ForumView() { ID = 2, Name = "aaa", Description = "bbb" });
            dgForums.ItemsSource = forumsView;
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
            if (dgForums.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose Forum");
                return;
            }
            int id = ((ForumView) dgForums.Items.GetItemAt(dgForums.SelectedIndex)).ID;

        }
    }
}
