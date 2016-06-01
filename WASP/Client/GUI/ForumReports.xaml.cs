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
    /// Interaction logic for ForumReports.xaml
    /// </summary>
    public partial class ForumReports : Window
    {
        private ModeratorReport report; 
        public ForumReports()
        {
            InitializeComponent();
            try
            {
                report = Session.bl.moderatorReport();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
                report = new ModeratorReport();
            }
            foreach (Moderator mod in report.moderators)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = mod.user.userName;
                item.DataContext = mod;
                cmboxModerator.Items.Add(item);
            }
            foreach (User user in Session.forum.members.Values)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.userName;
                item.DataContext = user;
                cmboxMember.Items.Add(item);
            }
        }
        private TreeViewItem makePostTree(Post post)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "Title: " + post.title + " Author: " + post.author.name + " Date: " + post.publishedAt.ToShortDateString();
            treeItem.DataContext = post;
            treeItem.Items.Add(new TreeViewItem() { Header = post.content, IsEnabled = false });
            treeItem.Items.Add(new TreeViewItem() { Header = "Last Edit: " + post.editAt.ToShortDateString(), IsEnabled = false });
            treeItem.Items.Add(new TreeViewItem() { Header = "by " + post.author.name, IsEnabled = false });
            return treeItem;
        }
        private void cmboxModerator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboxModerator.SelectedIndex <= 0)
            {
                MessageBox.Show("Please choose moderator");
                return;
            }
            ComboBoxItem selectedItem = (ComboBoxItem)cmboxModerator.SelectedItem;
            Moderator mod = (Moderator)selectedItem.DataContext;

            lblAppointBy.Content = ((string)lblAppointBy.DataContext) + mod.appointBy.userName;
            lblDate.Content = ((string)lblDate.DataContext) + mod.appointDate.ToShortDateString();
            lblSubForum.Content = ((string)lblSubForum.DataContext) + Session.forum.subforums[mod.subForumID].name;

            treeModPost.Items.Clear();
            foreach(Post p in report.moderatorsPosts[mod.user.id])
            {
                TreeViewItem item = makePostTree(p);
                treeModPost.Items.Add(item);
            }
        }
        private void cmboxMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboxMember.SelectedIndex <= 0)
            {
                MessageBox.Show("Please choose member");
                return;
            }
            try
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cmboxMember.SelectedItem;
                User user = (User)selectedItem.DataContext;

                List<Post> postList = Session.bl.postsByMember(user.id);
                treeMemberPost.Items.Clear();
                foreach (Post p in postList)
                {
                    TreeViewItem item = makePostTree(p);
                    treeMemberPost.Items.Add(item);
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
