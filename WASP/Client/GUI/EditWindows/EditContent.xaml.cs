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
        Post curPost;
        public EditContent(Post p)
        {
            InitializeComponent();
            curPost = p;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Session.bl.editPost(Session.user.id, Session.forum.id, curPost.id, txtContent.Text);
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
