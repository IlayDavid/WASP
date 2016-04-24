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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ICL _cl;
        int _forumID;
        User user;
        private static readonly int ALL_FORUMS = -1;

        public Login(ICL cl, int forum_id)
        {
            _cl = cl;
            _forumID = forum_id;
            InitializeComponent();
        }

        //for super user.
        public Login(ICL cl)
        {
            _cl = cl;
            _forumID = ALL_FORUMS;
            InitializeComponent();
        }
        public User getUser()
        {
            return user;
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string pass = txtPassword.Text;
            string username = txtUsername.Text;

            if (_forumID == ALL_FORUMS)
                user = _cl.loginSU(username, pass);
            else
                user = _cl.login(username, pass, _forumID);    
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
