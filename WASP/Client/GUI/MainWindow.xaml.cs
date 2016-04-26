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

        private IBL _bl;
        private User _user = null;
        private List<Forum> _forums;
        private List<ForumView> _fView;
        public WelcomeWindow()
        {
            InitializeComponent();
            _bl = new BL();
            if (_bl.isInitialize() == 0)
            {
                CreateAdmin cAdmin = new CreateAdmin(_bl);
                cAdmin.ShowDialog();
            }
            _forums = _bl.getAllForums();
            _fView = ForumView.getView(_forums);
            _fView.Add(new ForumView() { ID = 2, Name = "aaa", Description = "bbb" });
            dgForums.ItemsSource = _fView;
        }

        private void btnLoginSU_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(_bl, Login.ALL_FORUMS);
            login.Title = "Login as SU";
            login.ShowDialog();
            _user = login.getUser();

            ChangeVisibilitySU();
        }
        private void btnLogOutSU_Click(object sender, RoutedEventArgs e)
        {
            var ans = MessageBox.Show("Do you want to log out?", "Save and Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes)
            {
                ChangeVisibilitySU();
                _user = null;
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
                Forum forum = null;// _bl.getForum(id);
                ForumWindow fWin = new ForumWindow(_user, forum, _bl);
                this.Hide();
                fWin.ShowDialog();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnNewForum_Click(object sender, RoutedEventArgs e)
        {
            AddForum addF = new AddForum(_bl, (SuperUser)_user);
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
    }
}
