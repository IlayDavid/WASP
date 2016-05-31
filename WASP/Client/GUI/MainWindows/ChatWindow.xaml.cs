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
            foreach(User user in Session.forum.members.Values)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = user.userName;
                item.DataContext = user;
                lstMembers.Items.Add(item);
            }
            if (lstMembers.Items.Count > 0)
                lstMembers.SelectedIndex = 0;
        }

        private void lstMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstMembers.SelectedIndex < 0)
            {
                MessageBox.Show("Please choose member");
                return;
            }
            User selectedUser = (User)((ListBoxItem)lstMembers.SelectedItem).DataContext;

            lstMessages.Items.Clear();
            StackPanel messageView = MakeMessageView(); 
            lstMessages.Items.Add(messageView);         
        }

        private StackPanel MakeMessageView()
        {
            StackPanel stackPanel = new StackPanel();
            TextBox l = new TextBox() { };
            Button b = new Button() { Content = "Send" };
            stackPanel.Children.Add(l); stackPanel.Children.Add(b);
            throw new NotImplementedException();
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
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
