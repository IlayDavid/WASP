using System.Windows;
using System.Windows.Controls;
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;
using System;
using Client.GUI.EditWindows;

namespace Client
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window, INotificable
    {
        public PostWindow()
        {
            InitializeComponent();
            if (Session.user != null)
            {
                welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                if (Session.user is SuperUser)
                    ChangeVisibilitySU();
                else if (Session.subForum.moderators.ContainsKey(Session.user.id))
                    ChangeVisibilityModerator();
                else
                    ChangeVisibilityUser();
            }

            Post p = Session.post;
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "Title: " + p.title + " Author: " + p.author.name + " Date: " + p.publishedAt.Date;
            treeItem.DataContext = p;
            treeItem.Items.Add(new TreeViewItem() { Header = p.content });
            treeItem.Items.Add(new TreeViewItem() { Header = "Last Edit: " + p.editAt.Date });
            postMesssages.Items.Add(treeItem);
            foreach (Post post in p.replies)
            {
                treeItem = new TreeViewItem();
                treeItem.Header = "Title: " + post.title + " Author: " + post.author.name + " Date: " +post.publishedAt.Date;
                treeItem.DataContext = post;
                treeItem.Items.Add(new TreeViewItem() { Header = post.content});
                treeItem.Items.Add(new TreeViewItem() { Header = "Last Edit: " + post.editAt.Date });
                treeItem.Items.Add(new TreeViewItem() { Header = "by " + post.inReplyTo.author.name });
                postMesssages.Items.Add(treeItem);
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
            //acording to policy
            ChangeVisibilityAdmin();
        }
        private void ChangeVisibilityAdmin()
        {
            //acording to policy
            ChangeVisibilityModerator();
        }

        private void ChangeVisibilityModerator()
        {
            ChangeVisibilityUser();
        }

        private void ChangeVisibilityUser()
        {
            reverseVisibility(btnDelete);
            reverseVisibility(btnEdit);
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem selected = (TreeViewItem)postMesssages.SelectedItem;
                Post p = (Post)selected.DataContext;
                if (p == null)
                    p = (Post)((TreeViewItem)selected.Parent).DataContext;
                Session.bl.deletePost(Session.user.id, Session.forum.id, p.id);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem selected = (TreeViewItem)postMesssages.SelectedItem;
                Post p = (Post)selected.DataContext;
                if (p == null)
                    p = (Post)((TreeViewItem)selected.Parent).DataContext;
                EditContent editC = new EditContent(p);
                editC.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
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
    }
}
