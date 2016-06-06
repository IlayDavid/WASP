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

        internal static void LoadFriends()
        {
            if (user is SuperUser)
                user.friends = new List<User>();
            else
                user.friends = bl.getFriends();
        }
        internal static void LoadForums()
        {
            forums = bl.getAllForums().ToDictionary(x => x.id);
        }

        internal static void LoadReplys()
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

        internal static void ShowNotifications(List<Notification> nots)
        {
            if (nots == null)
            {
                MessageBox.Show("No new Notifications");
                return;
            }
            string notsStr = "";
            foreach (Notification m in nots)
            {
                notsStr += m.message + "\n";
            }
            MessageBox.Show(notsStr, "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void NotifyWindows(List<Notification> nots)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                try
                {
                    if (App.Current.Windows[intCounter] is INotificable)
                    {
                        INotificable curr = (INotificable) App.Current.Windows[intCounter];
                        curr.NotifyWindow(nots);
                    }// curr.
                }
                catch { }
        }
        internal static void NotifyWindow(List<Notification> notifications, Button notsBtn)
        {
            notsBtn.Content = "Notifications (" + notifications.Count + ")";
            notsBtn.DataContext = notifications;
        }
    }
}
