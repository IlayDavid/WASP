﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;
using Client.GUI.DeleteWindows;
using Client.GUI.EditWindows;

namespace Client
{
    /// <summary>
    /// Interaction logic for SubForumWindow.xaml
    /// </summary>
    public partial class SubForumWindow : Window, INotificable
    {
        public SubForumWindow()
        {
            InitializeComponent();
            setVisibility();

            //presenting the subforums list 
            List<Post> posts = Session.bl.getThreads(Session.forum.id, Session.subForum.id, 0, 20);
            foreach (Post p in posts)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = p.title;
                newItem.DataContext = p;
                SubForumsThreads.Items.Add(newItem);
            }
        }

        private void setVisibility()
        {
            if (Session.user != null)
            {
                Session.setModerators();
                welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                if (Session.user is SuperUser)
                    ChangeVisibilitySU();
                else if (Session.subForum.moderators.ContainsKey(Session.user.id))
                    ChangeVisibilityModerator();
                else
                    ChangeVisibilityUser();
            }
        }

        private void ChangeVisibilitySU()
        {
            reverseVisibility(btnRemoveModerator);
            ChangeVisibilityModerator();
        }
        private void ChangeVisibilityModerator()
        {
            reverseVisibility(btnAddModerator);
            reverseVisibility(btnEditSubforumSettings);
            ChangeVisibilityUser();
        }

        private void ChangeVisibilityUser()
        {
            reverseVisibility(btnPostThread);
            reverseVisibility(btnRegister);
            reverseVisibility(btnLogin);
            reverseVisibility(btnLogout);
        }
        private void reverseVisibility(Button btn)
        {
            btn.Visibility = (btn.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Session.CloseAllWindows();
        }
        private void SubForumsThreads_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem i = (ListBoxItem)SubForumsThreads.SelectedItem;
            Post p = (Post)i.DataContext;
            Session.post = p;
            PostWindow pwin = new PostWindow();
            pwin.Title = p.title;

            Session.currentWindow = pwin;
            this.Hide();
            pwin.ShowDialog();
            Session.currentWindow = this;
            this.ShowDialog();
            Session.post = null;
        }


        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Session.forum.id);
            login.ShowDialog();
            if (Session.user == null)
                return;
            welcomeTextBlock.Text = "Welcome, " + Session.user.name;
            setVisibility();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (Session.user is SuperUser)
            {
                MessageBox.Show("Super user should log out only in the main window!");
                return;
            }
            var ans = MessageBox.Show("Do you want to log out?", "Save and Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes)
            {
                setVisibility();
                Session.user = null;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AddMember addM = new AddMember();
            addM.ShowDialog();
            Session.user = addM.getUser();
            welcomeTextBlock.Text = "Welcome, " + Session.user.name;
            setVisibility();
        }

        private void btnPostThread_Click(object sender, RoutedEventArgs e)
        {
            AddPost addPost = new AddPost(true);
            addPost.ShowDialog();
            Post newPost = addPost.getPost();

            if (newPost != null)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = newPost.title;
                newItem.DataContext = newPost;
                SubForumsThreads.Items.Add(newItem);
            }
        }

        private void btnViewModerators_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window moderatorView = new Window();
                DataGrid dg = new DataGrid();
                List<Moderator> mods = Session.bl.getModerators(Session.forum.id, Session.subForum.id);
                dg.ItemsSource = ModeratorView.getView(mods);
                dg.IsReadOnly = true;
                moderatorView.Content = dg;
                moderatorView.SizeToContent = SizeToContent.WidthAndHeight;
                moderatorView.Title = "Moderators";
                moderatorView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                moderatorView.ShowDialog();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnAddModerator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddModerator addModerator = new AddModerator();
                addModerator.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void btnRemoveModerator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteModerator deleteM = new DeleteModerator();
                deleteM.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void btnEditModeratorTerm_Click(object sender, RoutedEventArgs e)
        {
            EditTerm editT = new EditTerm();
            editT.ShowDialog();
        }
        private void btnEditSubforumSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
