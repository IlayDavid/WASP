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
using Client.DataClasses;
using Client.GUI;

namespace Client
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window, INotificable
    {
        public PostWindow()
        {
            InitializeComponent();

            Post p = Session.post;
            Post rep = new Post("Thread 1", "this is a reply post 1", Session.user, Session.subForum.Id, Session.post);
            
            TreeViewItem treeItem = new TreeViewItem();
            treeItem.Header = "Title: " + p._title + " Author: " + p._author.name + " Date: " + p._publishedAt.Date;
            treeItem.Items.Add(new TreeViewItem() { Header = p._content });
            treeItem.Items.Add(new TreeViewItem() { Header = p._editAt.Date });
            postMesssages.Items.Add(treeItem);
            foreach (Post post in p._replies)
            {
                TreeViewItem treeItem2 = new TreeViewItem();
                treeItem2.Header = "Title: " + post._title + " Author: " + post._author.name + " Date: " +post._publishedAt.Date;
                treeItem2.Items.Add(new TreeViewItem() { Header = post._content });
                treeItem2.Items.Add(new TreeViewItem() { Header = post._editAt.Date });
                treeItem2.Items.Add(new TreeViewItem() { Header =post._inReplyTo._author.name });
                postMesssages.Items.Add(treeItem2);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
