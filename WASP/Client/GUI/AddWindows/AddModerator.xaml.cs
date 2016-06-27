using Client.DataClasses;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddModerator.xaml
    /// </summary>
    public partial class AddModerator : Window
    {
        public AddModerator()
        {
            InitializeComponent();
            foreach (User user in Session.forum.members.Values)
            {
                if (modInOtherSF(user))
                    continue;
                if (Session.subForum.moderators.ContainsKey(user.id))
                    continue;
                if (user.joinDate.AddMonths(Session.forum.policy.seniority) > (DateTime.Now))
                    continue;
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = user.userName;
                newItem.DataContext = user;
                cboxModerator.Items.Add(newItem);
            }
        }
        private bool modInOtherSF(User user)
        {
            //return Session.bl.modAlready(user.id);
            return false;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cboxModerator.SelectedIndex <= 0)
            {
                MessageBox.Show("Please choose moderator");
                return;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cboxModerator.SelectedItem;
            int moderatorID = ((User)selectedItem.DataContext).id;
            try
            {
                Session.bl.addModerator(moderatorID, Session.subForum.id, calTerm.SelectedDate.Value);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
