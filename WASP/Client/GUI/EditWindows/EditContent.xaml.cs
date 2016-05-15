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
    /// Interaction logic for EditContent.xaml
    /// </summary>
    public partial class EditContent : Window
    {
        private Post curPost;
        private string postContent;

        public EditContent(Post p)
        {
            InitializeComponent();
            txtContent.Text = p.content;
            curPost = p;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                postContent = txtContent.Text;
                Session.bl.editPost(curPost.id, postContent);
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        public string getPostContent()
        {
            return postContent;
        }
    }
}
