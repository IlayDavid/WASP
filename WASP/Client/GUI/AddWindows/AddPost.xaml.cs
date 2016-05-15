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

namespace Client.GUI.AddWindows
{
    /// <summary>
    /// Interaction logic for AddPost.xaml
    /// </summary>
    public partial class AddPost : Window
    {
        private Post _replyTo;
        private Post _post;

        public AddPost(Post replyTo)
        {
            _replyTo = replyTo;
            InitializeComponent();
            if (_replyTo == null)
            {
                lblTitle.Visibility = Visibility.Visible;
                txtTitle.Visibility = Visibility.Visible;
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_replyTo == null)
                    _post = Session.bl.createThread(txtTitle.Text, txtContent.Text, Session.subForum.id);
                else
                    _post = Session.bl.createReplyPost(txtContent.Text, _replyTo.id);
                this.Close();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public Post getPost()
        {
            return _post;
        }
    }
}
