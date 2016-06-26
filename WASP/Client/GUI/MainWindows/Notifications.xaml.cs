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
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Window
    {
        public Notifications()
        {
            InitializeComponent();
            foreach(Notification n in Session.notfications)
            {
                TreeViewItem item = new TreeViewItem();
                item = makePostTree(n);
                lstBoxNots.Items.Add(item);
            }
        }
        private TreeViewItem makePostTree(Notification not)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "From: " + not.source.userName + " Date: " + not.creationTime.ToShortDateString();
            treeItem.DataContext = not;
            treeItem.Items.Add(new TreeViewItem() { Header = not.message, IsEnabled = false });
            return treeItem;
        }
    }
}
