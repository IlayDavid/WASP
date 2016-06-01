using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;
using Client.GUI.EditWindows;
using System.Collections.Generic;
using System.Linq;
using Client.GUI.MainWindows;

namespace Client
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window, INotificable
    {
        List<Button> guestBtns;
        List<Button> userBtns;
        List<Button> adminBtns;
        List<Button> suBtns;

        private void setButtons()
        {
            guestBtns = new List<Button>() { btnRegister, btnLogin };
            userBtns = new List<Button>() { btnLogout, btnSendMessage };
            adminBtns = new List<Button>() { btnAddAdministrator, btnEditForumPolicy, btnAddSubforum };
            suBtns = new List<Button>() {  };

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
        //the forum that presented in the window, should be set by method
        public ForumWindow()
        {
            InitializeComponent();
            setButtons();
            setVisibility();
            LoadData();
            //presenting the subforums list 
            foreach (Subforum sf in Session.forum.subforums.Values)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = sf.name;
                newItem.DataContext = sf;
                SubForums.Items.Add(newItem);
            }
            if (Session.forum.subforums.Count > 0)
                SubForums.SelectedIndex = 0;
        }

        public void LoadData()
        {
            Session.LoadMembers();
            Session.LoadSubForums();
            Session.LoadAdmins();
            Session.LoadMembers();
        }
 
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Session.CloseAllWindows();
        }
        public void setForum(Forum f)
        {
            Session.forum = f;
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();         
        }
        private void btnViewMembers_Click(object sender, RoutedEventArgs e)
        {
            Window membersView = new Window();
            DataGrid dgMembers = new DataGrid();
            dgMembers.ItemsSource = UserView.getView(Session.forum.members);
            dgMembers.IsReadOnly = true;

            DataGrid dgAdmins = new DataGrid();
            dgAdmins.ItemsSource = UserView.getView(Session.forum.admins.Values.Select(x => x.user).ToList());
            dgAdmins.IsReadOnly = true;

            DataGrid dgFriends = new DataGrid();
            if (Session.user != null)
            {
                dgFriends.ItemsSource = UserView.getView(Session.user.friends);
                dgFriends.IsReadOnly = true;
            }

            StackPanel mainSp = new StackPanel();
            StackPanel rightSp = new StackPanel();
            StackPanel leftSp = new StackPanel();

            leftSp.Children.Add(new Label() { Content = "Members:" });
            leftSp.Children.Add(dgMembers);
            leftSp.Children.Add(new Label() { Content = "Administrators:" });
            leftSp.Children.Add(dgAdmins);
            leftSp.Children.Add(new Label() { Content = "  " });

            Button btnAddFriends = new Button() { Content = "Add Friend", DataContext = dgMembers };
            btnAddFriends.Click += new System.Windows.RoutedEventHandler(this.btnAddFriends_Click);

            if (Session.user != null)
            {
                rightSp.Children.Add(new Label() { Content = "Friends:" });
                rightSp.Children.Add(dgFriends);
                rightSp.Children.Add(new Label() { Content = "In order to add friend,\nchoose member record\nand press the button." });
                rightSp.Children.Add(btnAddFriends);
            }
           
            mainSp.Orientation = Orientation.Horizontal;
            mainSp.Children.Add(new Label() { Content = "  " });
            mainSp.Children.Add(leftSp);
            mainSp.Children.Add(new Label() { Content = "             "});
            mainSp.Children.Add(rightSp);
            mainSp.Children.Add(new Label() { Content = "  " });

            membersView.Content = mainSp;
            membersView.SizeToContent = SizeToContent.WidthAndHeight;
            membersView.Title = "Members & Friends";
            membersView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            membersView.ShowDialog();
        }
        private void btnAddFriends_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGrid dgMembers = (DataGrid)((Button)sender).DataContext;
                UserView selectedUser = (UserView)dgMembers.SelectedItem;
                if (selectedUser == null)
                {
                    MessageBox.Show("Please select a friend", "Add Friend", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int id = Session.forum.members.First(x => (x.Value.userName == selectedUser.UserName)).Value.id;
                Session.bl.addFriend(id);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAddAdministrator_Click(object sender, RoutedEventArgs e)
        {
            AddAdmin addAdmin = new AddAdmin();
            addAdmin.ShowDialog();
            Session.LoadAdmins();
        }

        private void btnAddSubforum_Click(object sender, RoutedEventArgs e)
        {
            AddSubForum addSf = new AddSubForum();
            addSf.ShowDialog();
            Subforum newSf = addSf.getSubForum();
            if (newSf != null)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = newSf.name;
                newItem.DataContext = newSf;
                SubForums.Items.Add(newItem);
                Session.LoadSubForums();
            }
        }

        private void btnEditForumPolicy_Click(object sender, RoutedEventArgs e)
        {
            EditPolicy editP = new EditPolicy();
            editP.ShowDialog();           
        }

        private void SubForums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem i = (ListBoxItem)SubForums.SelectedItem;
            Subforum sf = (Subforum)i.DataContext;
            if (sf == null)
            {
                MessageBox.Show("Please select a Sub-Forum");
                return;
            }

            Session.subForum = sf;
            SubForumWindow sfWin = new SubForumWindow();
            sfWin.Title = sf.name;
            Session.currentWindow = sfWin;
            this.Hide();
            sfWin.ShowDialog();
            setVisibility();
            this.ShowDialog();
            Session.subForum = null;
            Session.currentWindow = this;
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Session.forum.id);
            login.ShowDialog();
            if (Session.user == null)
                return;
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
        private void notificationsButton_Click(object sender, RoutedEventArgs e)
        {
            Session.ShowNotifications((List<Notifications>)notificationsButton.DataContext); 
        }
        void NotifyWindow(List<Notifications> notifications, Button notsBtn)
        {
            Session.NotifyWindow(notifications, notsBtn);
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow chat = new ChatWindow();
            chat.ShowDialog();
        }
    }
}
