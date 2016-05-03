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
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;

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
            Post rep = new Post("Thread 1", "this is a reply post 1", Session.user, Session.subForum.id, Session.post);
            
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "Title: " + p.title + " Author: " + p.author.name + " Date: " + p.publishedAt.Date;
            treeItem.Items.Add(new TreeViewItem() { Header = p.content });
            treeItem.Items.Add(new TreeViewItem() { Header = p.editAt.Date });
            postMesssages.Items.Add(treeItem);
            foreach (Post post in p.replies)
            {
                TreeViewItem treeItem2 = new TreeViewItem();
                treeItem2.Header = "Title: " + post.title + " Author: " + post.author.name + " Date: " +post.publishedAt.Date;
                treeItem2.Items.Add(new TreeViewItem() { Header = post.content });
                treeItem2.Items.Add(new TreeViewItem() { Header = post.editAt.Date });
                treeItem2.Items.Add(new TreeViewItem() { Header =post.inReplyTo.author.name });
                postMesssages.Items.Add(treeItem2);
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

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Session.forum.id);
            login.ShowDialog();
            if (Session.user == null)
                return;
            welcomeTextBlock.Text = "Welcome, " + Session.user.name;
            ChangeVisibilityUser();
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
                ChangeVisibilityUser();
                Session.user = null;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AddMember addM = new AddMember();
            addM.ShowDialog();
            Session.user = addM.getUser();
            welcomeTextBlock.Text = "Welcome, " + Session.user.name;
            ChangeVisibilityUser();
        }
    }
}
