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
            Session.LoadMessages();
        }

        private void lstMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstMembers.SelectedIndex < 0)
            {
                MessageBox.Show("Please choose member");
                return;
            }
            User selectedUser = (User)((ListBoxItem)lstMembers.SelectedItem).DataContext;

            Session.AddNewNotifications();
            lstMessages.Items.Clear();
            foreach (Notification msg in Session.messages)
            {
                if (msg.sourceID == Session.user.id && msg.targetID == selectedUser.id)
                {
                    ListBoxItem item = new ListBoxItem() { Content = "YOU: " + msg.message };
                    lstMessages.Items.Add(item);
                }
                if (msg.sourceID == selectedUser.id && msg.targetID == Session.user.id)
                {
                    ListBoxItem item = new ListBoxItem() { Content = selectedUser.userName + ": " + msg.message };
                    lstMessages.Items.Add(item);
                }
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
