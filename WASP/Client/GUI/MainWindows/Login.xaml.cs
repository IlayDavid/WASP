﻿using Client.BusinessLogic;
using Client.CommunicationLayer;
using Client.DataClasses;
using Client.GUI;
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
        int _forumID;
        User _user;
        public static readonly int ALL_FORUMS = -1;

        public Login(int loginTo)
        {
            _forumID = loginTo;
            InitializeComponent();
        }
        
        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = passPassword.Password;
                if (_forumID == ALL_FORUMS)
                    _user = Session.bl.loginSU(username, password);
                else
                    _user = Session.bl.login(username, password, _forumID);
                Session.user = _user;
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
