using Client.BusinessLogic;
using Client.CommunicationLayer;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Client.GUI
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private List<ForumView> _fView;
        List<Button> guestBtns;
        List<Button> suBtns;

        private void setButtons()
        {
            guestBtns = new List<Button>() { btnLoginSU };
            suBtns = new List<Button>() { btnLogOut, btnDelete, btnNewForum, btnReports };
        }
        private void setVisibility()
        {
            setBtnVisibility(guestBtns, Visibility.Hidden);
            if (Session.user != null)
            {
                if (Session.user is SuperUser)
                    ChangeVisibilitySU();
            }
            else
                ChangeVisibilityGuest();
        }
        private void ChangeVisibilitySU()
        {
            setBtnVisibility(suBtns, Visibility.Visible);
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
        public WelcomeWindow()
        {
            InitializeComponent();
            setButtons();
            setVisibility();
            Session.bl = new BL();
            if (Session.bl.isInitialize() == 0)
            {
                CreateAdmin cAdmin = new CreateAdmin();
                cAdmin.ShowDialog();
            }
            refresh();
        }

        private void refresh()
        {
            Session.LoadForums();
            _fView = ForumView.getView(Session.forums.Values.ToList());
            dgForums.ItemsSource = _fView;
        }

        private void btnLoginSU_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Login.ALL_FORUMS);
            login.Title = "Login as SU";
            login.ShowDialog();

            if (Session.user != null)
            {
                setVisibility();
                MessageBox.Show("You are login as super user, you can log out only in this window.");
            }
        }
        private void btnLogOutSU_Click(object sender, RoutedEventArgs e)
        {
            var ans = MessageBox.Show("Do you want to log out?", "Save and Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes)
            {
                Session.user = null;
                setVisibility();  
            }
        }

        

        private void reverseVisibility(Button btn)
        {
            btn.Visibility = (btn.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnEnterForum_Click(object sender, RoutedEventArgs e)
        {
            if (dgForums.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Forum");
                return;
            }
            int id = ((ForumView) dgForums.Items.GetItemAt(dgForums.SelectedIndex)).ID;

            try
            {
                if (Session.user is SuperUser)
                    Session.bl.setForumID(id);
                Session.forum = Session.forums[id];
                ForumWindow fWin = new ForumWindow();
                fWin.Title = Session.forum.Name;
                
                Session.currentWindow = fWin;
                this.Hide();
                fWin.ShowDialog();

                setVisibility();
                
                Session.currentWindow = null; //do not need notifications.
                Session.forum = null;
                this.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnNewForum_Click(object sender, RoutedEventArgs e)
        {
            AddForum addF = new AddForum();
            addF.ShowDialog();
            Forum tmpF = addF.getForum();
            if (tmpF != null)
            {
                _fView.Add(new ForumView() { ID = tmpF.id, Name = tmpF.Name, Description = tmpF.Description });
                refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgForums.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Forum");
                return;
            }
            int id = ((ForumView)dgForums.Items.GetItemAt(dgForums.SelectedIndex)).ID;

            //Session.bl.deleteForum();
            refresh();
            MessageBox.Show("Did not require in use cases");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Session.CloseAllWindows();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int totalf = Session.bl.totalForums();


                Window reportView = new Window();
                DataGrid dg = new DataGrid();

                var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
                stackPanel.Children.Add(new Label { Content = "Total Forums: " + totalf + "\n" });
                stackPanel.Children.Add(new Label { Content = "Members that exist in the same forums: " });

                //List<UserView> l = new List<UserView>();
                //l.Add(new UserView() { ID = 12345, Email="a", Name="b", UserName="c"});
                //dg.ItemsSource = l;
                dg.ItemsSource = UserView.getView(Session.bl.membersInDifferentForums());
                dg.IsReadOnly = true;
                stackPanel.Children.Add(dg);
                reportView.Content = stackPanel;
                reportView.SizeToContent = SizeToContent.WidthAndHeight;
                reportView.Title = "SU Reports";
                reportView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                reportView.ShowDialog();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}