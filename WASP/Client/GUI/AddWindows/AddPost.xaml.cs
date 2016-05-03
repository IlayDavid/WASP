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
        private bool _isOpening;
        private Post _post;

        public AddPost(bool isOpening)
        {
            _isOpening = isOpening;
            InitializeComponent();
            if (_isOpening)
            {
                lblTitle.Visibility = Visibility.Visible;
                txtTitle.Visibility = Visibility.Visible;
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isOpening)
                    _post = Session.bl.createThread(Session.user.id, Session.forum.id, txtTitle.Text, txtContent.Text, Session.subForum.id);
                else
                    _post = Session.bl.createReplyPost(Session.user.id, Session.forum.id, txtContent.Text, Session.post.id);
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
