using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.DataClasses;
using Client.GUI;
using Client.GUI.AddWindows;
using Client.GUI.EditWindows;

namespace Client
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window, INotificable
    {
        

        //the forum that presented in the window, should be set by method
        public ForumWindow()
        {
            InitializeComponent();

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
        }
        public void LoadData()
        {
            Session.LoadMembers();
            Session.LoadSubForums();
            Session.LoadAdmins();
            Session.LoadMembers();
        }
        private void setVisibility()
        {
            if (Session.user != null)
            {
                Session.LoadAdmins();
                if (Session.user != null )
                {
                    welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                    if (Session.user is SuperUser)
                        ChangeVisibilitySU();
                    else if (Session.forum.admins.ContainsKey(Session.user.id))
                        ChangeVisibilityAdmin();
                    else
                        ChangeVisibilityUser();
                }
            }
        }
        private void ChangeVisibilitySU()
        {
            reverseVisibility(btnAddAdministrator);
            reverseVisibility(btnAddSubforum);
            reverseVisibility(btnEditForumPolicy);

            ChangeVisibilityAdmin();
        }

        private void ChangeVisibilityAdmin()
        {
            ChangeVisibilityUser();
        }

        private void ChangeVisibilityUser()
        {
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
        public void setForum(Forum f)
        {
            Session.forum = f;
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();         
        }

        private void btnViewAdministrators_Click(object sender, RoutedEventArgs e)
        {
            Window adminView = new Window();
            DataGrid dg = new DataGrid();
            dg.ItemsSource = UserView.getView(Session.forum.admins);
            dg.IsReadOnly = true;
            adminView.Content = dg;
            adminView.SizeToContent = SizeToContent.WidthAndHeight;
            adminView.Title = "Administators";
            adminView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminView.ShowDialog();
        }

        private void btnAddAdministrator_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("It not require yet!");
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
            Session.subForum = sf;
            SubForumWindow sfWin = new SubForumWindow();
            sfWin.Title = sf.name;
            Session.currentWindow = sfWin;
            this.Hide();
            sfWin.ShowDialog();
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
                setVisibility();
                Session.user = null;
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

        }

    }
}
