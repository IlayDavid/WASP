using System.Windows;
using System.Windows.Controls;
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;
using System;
using Client.GUI.EditWindows;
using System.Collections.Generic;

namespace Client
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window, INotificable
    {
        private readonly int CONTENT_IND = 0;
        //private readonly int EDIT_AT_IND = 0;
        //private readonly int BY_IND = 0;
        List<Button> guestBtns;
        List<Button> userBtns;
        List<Button> adminBtns;
        List<Button> suBtns;

        private void setButtons()
        {
            guestBtns = new List<Button>() { btnRegister, btnLogin };
            userBtns = new List<Button>() { btnLogout , btnAddReply, btnEdit, btnDelete};
            adminBtns = new List<Button>();
            suBtns = new List<Button>();

            adminBtns.AddRange(userBtns);
            suBtns.AddRange(adminBtns);
        }
        private void setVisibility()
        {
            if (Session.user != null)
            {
                Session.LoadAdmins();
                welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                if (Session.user is SuperUser)
                    ChangeVisibilitySU();
                else if (Session.forum.admins.ContainsKey(Session.user.id))
                    ChangeVisibilityAdmin();
                else
                    ChangeVisibilityUser();
            }
            else
                ChangeVisibilityGuest();
        }
        private void ChangeVisibilitySU()
        {
            setBtnVisibility(suBtns, Visibility.Visible);
        }

        private void ChangeVisibilityAdmin()
        {
            setBtnVisibility(suBtns, Visibility.Hidden);
            setBtnVisibility(adminBtns, Visibility.Visible);
        }

        private void ChangeVisibilityUser()
        {
            setBtnVisibility(suBtns, Visibility.Hidden);
            setBtnVisibility(userBtns, Visibility.Visible);
        }
        private void ChangeVisibilityGuest()
        {
            setBtnVisibility(suBtns, Visibility.Hidden);
            setBtnVisibility(guestBtns, Visibility.Visible);
        }

        private void setBtnVisibility(List<Button> btns, Visibility option)
        {
            foreach (Button btn in btns)
            {
                btn.Visibility = option;
            }
        }

        public PostWindow()
        {
            InitializeComponent();
            setButtons();
            setVisibility();

            Session.LoadReplys();
            Post p = Session.post;
            TreeViewItem treeItem = makePostTree(p);
            postMesssages.Items.Add(treeItem);
            foreach (Post post in p.replies)
            {
                treeItem = makePostTree(post);
                ((TreeViewItem) postMesssages.Items[0]).Items.Add(treeItem);
            }
        }
        

        private TreeViewItem makePostTree(Post post)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "Title: " + post.title + " Author: " + post.author.name + " Date: " + post.publishedAt.ToShortDateString();
            treeItem.DataContext = post;
            treeItem.Items.Add(new TreeViewItem() { Header = post.content, IsEnabled = false});
            treeItem.Items.Add(new TreeViewItem() { Header = "Last Edit: " + post.editAt.ToShortDateString(), IsEnabled = false });
            treeItem.Items.Add(new TreeViewItem() { Header = "by " + post.author.name, IsEnabled = false });
            return treeItem;
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
                int isSuc = Session.bl.deletePost(p.id);
                if (isSuc > 0)
                {
                    if (selected.Parent is TreeViewItem)
                        ((TreeViewItem)selected.Parent).Items.Remove(selected);
                    else
                        postMesssages.Items.Remove(selected);
                }
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
                EditContent editC = new EditContent(p);
                editC.ShowDialog();
                ((TreeViewItem)selected.Items[CONTENT_IND]).Header = editC.getPostContent();
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
                Session.user = null;
                setVisibility();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AddMember addM = new AddMember();
            addM.ShowDialog();
            if (Session.user != null)
            {
                Session.LoadMembers();
                welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                setVisibility();
            }
        }

        private void postMesssages_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                TreeViewItem selected = (TreeViewItem)postMesssages.SelectedItem;
                if(selected == null)
                {
                    MessageBox.Show("Please select a post");
                    return;
                }
                if (selected.Items.Count <= 3)
                {
                    Post post = (Post)selected.DataContext;
                    List<Post> replys = Session.bl.getReplys(post.id);

                    foreach (Post p in replys)
                    {
                        selected.Items.Add(makePostTree(p));
                    }
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnAddReply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem selected = (TreeViewItem)postMesssages.SelectedItem;
                Post post = (Post)selected.DataContext;
                if(post == null)
                {
                    MessageBox.Show("Please choose post");
                    return;
                }
                AddPost addP = new AddPost(post);
                addP.ShowDialog();
                Post p = addP.getPost();

                if(p != null)
                {
                    TreeViewItem tvi = makePostTree(p);
                    tvi.IsExpanded = true;
                    selected.Items.Add(tvi);
                }
                    
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void notificationsButton_Click(object sender, RoutedEventArgs e)
        {
            Session.ShowNotifications((List<Notifications>)notificationsButton.DataContext);
        }
        void NotifyWindow(List<Notifications> notifications, Button notsBtn)
        {
            Session.NotifyWindow(notifications, notsBtn);
        }
    }
}
