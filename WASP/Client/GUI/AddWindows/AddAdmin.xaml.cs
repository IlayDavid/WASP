using Client.DataClasses;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddAdmin.xaml
    /// </summary>
    public partial class AddAdmin : Window
    {
        public AddAdmin()
        {
            InitializeComponent();
            foreach (User user in Session.forum.members.Values)
            {
                if (Session.forum.admins.ContainsKey(user.id))
                    continue;
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = user.userName;
                newItem.DataContext = user;
                cboxAdmin.Items.Add(newItem);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cboxAdmin.SelectedIndex <= 0)
            {
                MessageBox.Show("Please choose admin");
                return;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cboxAdmin.SelectedItem;
            User admin = ((User)selectedItem.DataContext);
            try
            {
                Session.bl.addAdmin(admin.id);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
