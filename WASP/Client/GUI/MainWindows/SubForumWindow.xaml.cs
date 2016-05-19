using System;
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
        List<Button> guestBtns;
        List<Button> userBtns;
        List<Button> adminBtns;
        List<Button> suBtns;

        private void setButtons()
        {
            guestBtns = new List<Button>() { btnRegister, btnLogin };
            userBtns = new List<Button>() { btnLogout, btnPostThread};
            adminBtns = new List<Button>() {btnAddModerator, btnEditModeratorTerm, btnRemoveModerator, btnRepots };
            suBtns = new List<Button>();

            adminBtns.AddRange(userBtns);
            suBtns.AddRange(adminBtns);
        }
        private void setVisibility()
        {
            setBtnVisibility(guestBtns, Visibility.Hidden);
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
        public SubForumWindow()
        {
            InitializeComponent();
            setButtons();
            setVisibility();
            LoadData();
            //presenting the subforums list 
            RefreshWindow();
        }
        private void RefreshWindow()
        {
            SubForumsThreads.Items.Clear();
            foreach (Post p in Session.subForum.threads)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = p.title;
                newItem.DataContext = p;
                SubForumsThreads.Items.Add(newItem);
            }
            if (Session.subForum.threads.Count > 0)
                SubForumsThreads.SelectedIndex = 0;
        }

        private void LoadData()
        {
            Session.LoadModerators();
            Session.LoadThreads();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Session.CloseAllWindows();
        }
        private void SubForumsThreads_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SubForumsThreads.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a post");
                return;
            }
            ListBoxItem i = (ListBoxItem)SubForumsThreads.SelectedItem;
            Post p = (Post)i.DataContext;
            
            Session.post = p;
            PostWindow pwin = new PostWindow();
            pwin.Title = p.title;

            Session.currentWindow = pwin;
            this.Hide();
            pwin.ShowDialog();
            Session.LoadThreads();
            RefreshWindow();
            Session.currentWindow = this;
            setVisibility();
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

        private void btnPostThread_Click(object sender, RoutedEventArgs e)
        {
            AddPost addPost = new AddPost(null);
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
                List<Moderator> mods = Session.bl.getModerators(Session.subForum.id);
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
            AddModerator addModerator = new AddModerator();
            addModerator.ShowDialog();
            Session.LoadModerators();
        }
        private void btnRemoveModerator_Click(object sender, RoutedEventArgs e)
        {
            DeleteModerator deleteM = new DeleteModerator();
            deleteM.ShowDialog();
            Session.LoadModerators();
        }
        private void btnEditModeratorTerm_Click(object sender, RoutedEventArgs e)
        {
            EditTerm editT = new EditTerm();
            editT.ShowDialog();
            Session.LoadModerators();
        }

        private void btnRepots_Click(object sender, RoutedEventArgs e)
        {
            int num = Session.bl.subForumTotalMessages(Session.subForum.id);
            MessageBox.Show("Total number of posts in Sub Forum \"" + Session.subForum.name + "\" is: " + num);
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
