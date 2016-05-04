using Client.BusinessLogic;
using Client.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System;

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
            post.replies = bl.getReplys(0, 0, post.id);
        }

        internal static void LoadThreads()
        {
            subForum.threads = bl.getThreads(forum.id, subForum.id, 0, 20);
        }

        internal static void LoadSubForums()
        {
            forum.subforums = bl.getSubforums(forum.id).ToDictionary(x => x.id);
        }

        public static void LoadModerators()
        {
            subForum.moderators = bl.getModerators(0, subForum.id).ToDictionary(x => x.user.id);
        }

        public static void LoadMembers()
        {
            if (user != null)
                forum.members = bl.getMembers(user.id, forum.id).ToDictionary(x => x.id);
        }

        public static void LoadAdmins()
        {
            //if(user != null)
            //forum.admins = bl.getAdmins(user.id, forum.id).Select(x => x.user).ToList().ToDictionary(x => x.id);
        }

        public static void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
