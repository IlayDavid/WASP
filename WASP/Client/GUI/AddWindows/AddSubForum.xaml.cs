using Client.BusinessLogic;
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

namespace Client.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddSubForum : Window
    {
        private Subforum sf = null;
        public AddSubForum()
        {
            InitializeComponent();
            foreach(User user in Session.forum.members.Values)
            {
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
                MessageBox.Show("Please choose moderator to the subforum");
                return;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cboxModerator.SelectedItem;
            int moderatorID = ((User)selectedItem.DataContext).id;
            try
            {
                sf = Session.bl.createSubForum(Session.user.id, Session.forum.id, txtName.Text,
                    txtDescription.Text, moderatorID, calTerm.SelectedDate.Value);
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        internal Subforum getSubForum()
        {
            return sf;
        }
    }
}
