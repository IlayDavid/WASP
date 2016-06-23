using Client.BusinessLogic;
using Client.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using Client.GUI.MainWindows;

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

        public static List<Notification> nots = new List<Notification>();
        public static List<Notification> messages = new List<Notification>();


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

        internal static void ShowNotifications()
        {
            if (nots == null)
            {
                MessageBox.Show("No new Notifications");
                return;
            }

            Notifications notWin = new Notifications();
            notWin.ShowDialog();

            string notsStr = "";
            foreach (Notification m in nots)
            {
                notsStr += m.message + "\n";
            }
            MessageBox.Show(notsStr, "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void AddNewNotifications(List<Notification> nots)
        {
            foreach(Notification n in nots)
            {
                if (n.type == Notification.Types.Message)
                    messages.Add(n);
                else
                    nots.Add(n);
            }

            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                try
                {
                    if (App.Current.Windows[intCounter] is INotificable)
                    {
                        INotificable curr = (INotificable) App.Current.Windows[intCounter];
                        curr.NotifyWindow();
                    }
                }
                catch { }
        }
        public static void ClearNotifications()
        {
            nots.Clear();
            messages.Clear();

            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                try
                {
                    if (App.Current.Windows[intCounter] is INotificable)
                    {
                        INotificable curr = (INotificable)App.Current.Windows[intCounter];
                        curr.ClearNotification();
                    }
                }
                catch { }
        }

        internal static void NotifyWindow(Button btnNots)
        {
            btnNots.Content = "Notifications (" + nots.Count + ")";
        }
        internal static void ClearNotification(Button btnNots)
        {
            btnNots.Content = "Notifications (0)";
        }
        internal static void LoadPolicy()
        {
            Forum f = bl.getForum(forum.id);
            forum.policy = f.policy;
        }
    }
}
