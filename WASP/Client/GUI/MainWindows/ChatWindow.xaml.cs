using Client.DataClasses;
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

namespace Client.GUI.MainWindows
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
            Session.LoadFriends();
            foreach(User user in Session.user.friends)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = user.userName;
                item.DataContext = user;
                lstMembers.Items.Add(item);
            }
            if (lstMembers.Items.Count > 0)
                lstMembers.SelectedIndex = 0;

            lstMembers.SelectionChanged += lstMembers_SelectionChanged;
        }

        private void lstMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstMembers.SelectedIndex < 0)
            {
                MessageBox.Show("Please choose member");
                return;
            }
            User selectedUser = (User)((ListBoxItem)lstMembers.SelectedItem).DataContext;

            Session.bl.getNewNotificationses();
            List<Notification> nots = Session.bl.getAllNotificationses();
            foreach (Notification n in nots)
            {
                MessageBox.Show(n.message);
                if (n.type == Notification.Types.Message )
                   // && n.sourceID != -1 && 
                   // (n.source.userName.Equals(selectedUser.userName)
                   // || n.target.userName.Equals(selectedUser.userName)))
                {
                    ListBoxItem item = new ListBoxItem() { Content = n.target.userName + ": " + n.message };
                    lstMessages.Items.Add(item);
                }
            } 
        }

        private StackPanel MakeMessageView()
        {
            StackPanel stackPanel = new StackPanel();
            TextBox l = new TextBox() { };
            Button b = new Button() { Content = "Send" };
            b.Click += new System.Windows.RoutedEventHandler(this.btnSend1_Click);
            b.DataContext = l;
            stackPanel.Children.Add(l); stackPanel.Children.Add(b);


            return stackPanel;
        }
        private void btnSend1_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)((Control)sender).DataContext;
            MessageBox.Show(tb.Text);
            try
            {
                User user = (User)((ListBoxItem)lstMembers.SelectedItem).DataContext;
                Session.bl.sendMessage(user.id, txtMessage.Text);
                txtMessage.Clear();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (lstMembers.SelectedIndex < 0)
            {
                MessageBox.Show("Please choose member");
                return;
            }
            try
            {
                User user = (User)((ListBoxItem)lstMembers.SelectedItem).DataContext;
                Session.bl.sendMessage(user.id, txtMessage.Text);
                txtMessage.Clear();
                lstMembers_SelectionChanged(null, null);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
