﻿using Client.GUI;
using System;
using System.Windows;

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
            rdbUserPass.IsChecked = true;
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdbClientSession.IsChecked.Value)
                {
                    string session = txtClientSession.Text;
                    Session.user = Session.bl.loginBySession(session);
                    this.Close();
                }
                else
                {
                    string username = txtUsername.Text;
                    string password = passPassword.Password;
                    if (_forumID == ALL_FORUMS)
                        Session.user = Session.bl.loginSU(username, password);
                    else
                        Session.user = Session.bl.login(username, password, _forumID);
                    MessageBox.Show("Your client-session is: "+Session.user.client_session, "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void rdb_Checked(object sender, RoutedEventArgs e)
        {
            if(rdbClientSession.IsChecked.Value)
            {
                txtUsername.IsEnabled = false;
                passPassword.IsEnabled = false;
                txtClientSession.IsEnabled = true;
            }
            else
            {
                txtUsername.IsEnabled = true;
                passPassword.IsEnabled = true;
                txtClientSession.IsEnabled = false;
            }
        }
    }
}
