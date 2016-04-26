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
    /// Interaction logic for SubForumWindow.xaml
    /// </summary>
    public partial class SubForumWindow : Window
    {
        private Subforum sf;
        private Forum f;
        public SubForumWindow(Subforum Subf, Forum fo)
        {
            InitializeComponent();
            this.sf = Subf;
            this.f = fo;
            //testing the presentation on window
            sf._threads = new List<Post>();
            Post p1 = new Post();
            p1._title = "post no 1";
            Post p2 = new Post();
            p2._title = "post no 2";
            sf._threads.Add(p1);
            sf._threads.Add(p2);
            //testing end

            //presenting the subforums list 
            List<Post> posts = sf._threads;
            foreach (Post p in posts)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = p._title;
                newItem.DataContext = p;
                SubForumsThreads.Items.Add(newItem);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void setSubForum(Subforum subf)
        {
            this.sf= subf;
        }

        private void SubForumsThreads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem i = (ListBoxItem)SubForumsThreads.SelectedItem;
            Post p =(Post)i.DataContext;
            PostWindow pwin = new PostWindow(p, sf, f);
            pwin.Show();
            this.Close();

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            //ForumWindow fwin = new ForumWindow();
            //fwin.setForum(this.f);
            //fwin.Show();
            //this.Close();
        }
    }
}
