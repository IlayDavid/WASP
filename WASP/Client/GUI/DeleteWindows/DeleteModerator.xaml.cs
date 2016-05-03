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

namespace Client.GUI.DeleteWindows
{
    /// <summary>
    /// Interaction logic for DeleteModerator.xaml
    /// </summary>
    public partial class DeleteModerator : Window
    {
        public DeleteModerator()
        {
            InitializeComponent();
            foreach (Moderator moder in Session.subForum.moderators.Values)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = moder.user.userName;
                newItem.DataContext = moder;
                cboxModerator.Items.Add(newItem);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
                int isDelete = Session.bl.deleteModerator(Session.user.id, Session.forum.id,
                    moderatorID, Session.subForum.id);
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
