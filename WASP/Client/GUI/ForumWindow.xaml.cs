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
using Client.BusinessLogic;
using Client.GUI;
using Client.GUI.AddWindows;

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
            
            if(Session.user != null && Session.user is SuperUser)
            {
                welcomeTextBlock.Text = "Welcome, " + Session.user.name;
                ChangeVisibilitySU();
            }
            Session.forum.subforums = Subforum.ListToDictionary(Session.bl.getSubforums(Session.forum.ID));
            //presenting the subforums list 
            foreach (Subforum sf in Session.forum.subforums.Values)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = sf.Name;
                newItem.DataContext = sf;
                SubForums.Items.Add(newItem);
            }
        }

        private void ChangeVisibilitySU()
        {
            reverseVisibility(btnAddAdministrator);
            reverseVisibility(btnAddSubforum);
            reverseVisibility(btnEditForumPolicy);

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
            this.Close();
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
        }

        private void btnAddSubforum_Click(object sender, RoutedEventArgs e)
        {
            AddSubForum addSf = new AddSubForum();
            addSf.ShowDialog();
            Subforum newSf = addSf.getSubForum();
            if (newSf != null)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = newSf.Name;
                newItem.DataContext = newSf;
                SubForums.Items.Add(newItem);
            }
        }

        private void btnEditForumPolicy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubForums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem i = (ListBoxItem)SubForums.SelectedItem;
            Subforum sf = (Subforum)i.DataContext;
            Session.subForum = sf;
            SubForumWindow sfWin = new SubForumWindow();
            sfWin.Title = sf.Name;
            Session.currentWindow = sfWin;
            this.Hide();
            sfWin.ShowDialog();
            this.ShowDialog();
            Session.subForum = null;
            Session.currentWindow = this;
        }

        
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AddMember addM = new AddMember();
            addM.ShowDialog();
            Session.user = addM.getUser();
            welcomeTextBlock.Text = "Welcome, " + Session.user.name;
            ChangeVisibilityUser();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Session.forum.ID);
            login.ShowDialog(); //should update session.
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
        private void notificationsButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
