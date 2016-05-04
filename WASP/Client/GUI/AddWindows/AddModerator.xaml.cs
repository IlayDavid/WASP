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

namespace Client.GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddModerator.xaml
    /// </summary>
    public partial class AddModerator : Window
    {
        private Moderator moderator;

        public AddModerator()
        {
            InitializeComponent();
            foreach (User user in Session.forum.members.Values)
            {
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
                moderator = Session.bl.addModerator(Session.user.id, Session.forum.id, 
                    moderatorID, Session.subForum.id, calTerm.SelectedDate.Value);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        internal Moderator getModerator()
        {
            return moderator;
        }
    }
}
