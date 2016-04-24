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

namespace Client
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        Post p;
        Subforum sf;
        Forum f;
        public PostWindow(Post po, Subforum subf, Forum fo)
        {
            InitializeComponent();
            this.p = po;
            this.sf = subf;
            this.f = fo;
            //testing
            User mem = new User();
            mem.name = "noam";
            p._author = mem;
            p._publishedAt = new DateTime();
            p._content = "this is a post";
            p._editAt = new DateTime();

            Post rep = new Post();
            User mem2 = new User();
            mem2.name = "edan";
            rep._author = mem2;
            rep._publishedAt = new DateTime();
            rep._content = "this is a reply post";
            rep._editAt = new DateTime();
            rep._inReplyTo = p;

            p._replies = new List<Post>();
            p._replies.Add(rep);
            //end 
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
            SubForumWindow subWin = new SubForumWindow(sf, f);
            subWin.Show();
            this.Close();
        }
    }
}
