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
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        //the forum that presented in the window, should be set by method
        private Forum forum;
        public ForumWindow()
        {
            InitializeComponent();
            //this.forum = f;
            //testing the presentation on window
            forum = new Forum();
            forum.subforums = new List<Subforum>();
            Subforum sf1 = new Subforum();
            sf1.Name = "sf1";
            Subforum sf2 = new Subforum();
            sf2.Name = "sf2";
            forum.subforums.Add(sf1);
            forum.subforums.Add(sf2);
            //testing end

            //presenting the subforums list 
            List<Subforum> subfs = forum.subforums;
            foreach (Subforum sf in subfs)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = sf.Name;
                newItem.DataContext = sf;
                SubForums.Items.Add(newItem);
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void setForum(Forum f)
        {
            this.forum = f;
        }

        private void SubForums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem i = (ListBoxItem)SubForums.SelectedItem;
            Subforum sf = (Subforum)i.DataContext;
            SubForumWindow sfWin = new SubForumWindow(sf, this.forum);
            sfWin.Show();
            this.Close();
        }

    }
}
