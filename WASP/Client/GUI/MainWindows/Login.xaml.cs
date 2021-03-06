﻿using Client.GUI;
using Client.GUI.MainWindows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        int _forumID;
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
                string session = txtClientSession.Text;
                string username = txtUsername.Text;
                string password = passPassword.Password;
                if (_forumID == ALL_FORUMS)
                    Session.user = Session.bl.loginSU(username, password);
                else
                    Session.user = Session.bl.login(username, password, _forumID, session);
                MessageBox.Show("Your client-session is: " + Session.user.client_session, "", MessageBoxButton.OK, MessageBoxImage.Information);
                Session.AddNewNotifications();
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        
        private void lblForget_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RestorePassword rp = new RestorePassword();
            this.Hide();
            rp.ShowDialog();
            this.Close();
        }
    }
}
