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
        private List<Forum> _forums;
        private List<ForumView> _fView;
        public WelcomeWindow()
        {
            InitializeComponent();
            Session.bl = new BL();
            if (Session.bl.isInitialize() == 0)
            {
                CreateAdmin cAdmin = new CreateAdmin();
                cAdmin.ShowDialog();
            }
            _forums = Session.bl.getAllForums();
            _fView = ForumView.getView(_forums);
            dgForums.ItemsSource = _fView;
        }

        private void btnLoginSU_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(Login.ALL_FORUMS);
            login.Title = "Login as SU";
            login.ShowDialog();
            Session.user = login.getUser();

            if (Session.user != null)
                ChangeVisibilitySU();
        }
        private void btnLogOutSU_Click(object sender, RoutedEventArgs e)
        {
            var ans = MessageBox.Show("Do you want to log out?", "Save and Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes)
            {
                ChangeVisibilitySU();
                Session.user = null;
            }
        }

        private void ChangeVisibilitySU()
        {
            reverseVisibility(btnLoginSU);
            reverseVisibility(btnLogOut);
            reverseVisibility(btnNewForum);
            reverseVisibility(btnDelete);
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
                Session.forum = Session.bl.getForum(id);
                ForumWindow fWin = new ForumWindow();
                fWin.Title = Session.forum.Name;
                this.Hide();
                fWin.ShowDialog();
                this.Show();
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
                _forums.Add(tmpF);
                _fView.Add(new ForumView() { ID = tmpF.ID, Name = tmpF.Name, Description = tmpF.Description });
                dgForums.ItemsSource = null;
                dgForums.ItemsSource = _fView;
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

            MessageBox.Show("Did not require in use cases");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
