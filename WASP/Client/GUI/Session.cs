using Client.BusinessLogic;
using Client.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.GUI
{
    public class Session
    {
		
        public readonly static int ALL = 0;
        public readonly static int MEMBER = 1;
        public readonly static int SUBFORUMS = 2;
        public readonly static int ADMINS = 3;

        public static IBL bl = null;
        public static User user = null;
        public static Forum forum = null;
        public static Subforum subForum = null;
        public static Post post = null;
        public static INotificable currentWindow = null;

        public static Dictionary<int, Forum> forums = null;
        public static Dictionary<int, Subforum> subforums = null;
        public static Dictionary<int, Post> posts = null;
        public static void LoadForums()
        {
            forums = bl.getAllForums().ToDictionary(x => x.id);
        }

        public static void LoadReplys()
        {
            post.replies = bl.getReplys(post.id);
        }

        internal static void LoadThreads()
        {
            subForum.threads = bl.getThreads(subForum.id);
        }

        internal static void LoadSubForums()
        {
            forum.subforums = bl.getSubforums(forum.id).ToDictionary(x => x.id);
        }

        public static void LoadModerators()
        {
            subForum.moderators = bl.getModerators(subForum.id).ToDictionary(x => x.user.id);
        }

        public static void LoadMembers()
        {
            forum.members = bl.getMembers(forum.id).ToDictionary(x => x.id);
        }

        public static void LoadAdmins()
        {
            forum.admins = bl.getAdmins(forum.id).ToDictionary(x => x.user.id);
        }

        public static void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                try
                {
                    App.Current.Windows[intCounter].Close();
                }
                catch{ }
        }

        internal static void ShowNotifications(List<Notifications> nots)
        {
            if (nots == null)
            {
                MessageBox.Show("No new Notifications");
                return;
            }
            string notsStr = "";
            foreach (Notifications m in nots)
            {
                notsStr += m.message + "\n";
            }
            MessageBox.Show("No new Notifications");
        }

        internal static void NotifyWindow(List<Notifications> notifications, Button notsBtn)
        {
            notsBtn.Content = "Notifications (" + notifications.Count + ")";
            notsBtn.DataContext = notifications;
        }
    }
}
