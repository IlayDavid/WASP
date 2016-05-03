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

namespace Client.GUI.EditWindows
{
    /// <summary>
    /// Interaction logic for EditTerm.xaml
    /// </summary>
    public partial class EditTerm : Window
    {
        public EditTerm()
        {
            InitializeComponent();
            foreach (Moderator moder in Session.subForum._moderators.Values)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = moder.user.userName;
                newItem.DataContext = moder;
                cboxModerator.Items.Add(newItem);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cboxModerator.SelectedIndex <= 0)
            {
                MessageBox.Show("Please choose moderator");
                return;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cboxModerator.SelectedItem;
            int moderatorID = ((Moderator)selectedItem.DataContext).user.id;
            try
            {
                int isChange = Session.bl.updateModeratorTerm(Session.user.id, Session.forum.ID,
                    moderatorID, Session.subForum.Id, calTerm.SelectedDate.Value);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
