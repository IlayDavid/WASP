using Client.BusinessLogic;
using Client.DataClasses;
using System.Collections.Generic;
using System.Linq;

namespace Client.GUI
{
    public class Session
    {
        public static IBL bl = null;
        public static User user = null;
        public static Forum forum = null;
        public static Subforum subForum = null;
        public static Post post = null;
        public static INotificable currentWindow = null;

        public static Dictionary<int, Forum> forums = null;
        public static Dictionary<int, Subforum> subforums = null;
        public static Dictionary<int, Post> posts = null;
        public static void AddForums(List<Forum> fList)
        {
            if (forums == null)
                forums = new Dictionary<int, Forum>();
            foreach (Forum f in fList)
            {
                forums.Add(f.id, f);
            }
        }
        public static void AddSubForums(List<Subforum> sfList)
        {
            if (subforums == null)
                subforums = new Dictionary<int, Subforum>();
            foreach (Subforum sf in sfList)
            {
                subforums.Add(sf.id, sf);
            }
        }
        public static void AddPosts(List<Post> pList)
        {
            posts = new Dictionary<int, Post>();
            foreach (Post p in pList)
            {
                posts.Add(p.id, p);
            }
        }

        public static void setModerators()
        {
            List<Moderator> list = bl.getModerators(0, subForum.id);
            subForum.moderators = new Dictionary<int, Moderator>();
            foreach (Moderator mod in list)
            {
                subForum.moderators.Add(mod.user.id, mod);
            }
        }

        internal static void setAdmins()
        {
            List<User> list = bl.getAdmins(user.id, forum.id).Select(x => x.user).ToList();
            forum.admins = new Dictionary<int, User>();
            foreach (User admin in list)
            {
                forum.admins.Add(admin.id, admin);
            }
        }

        public static void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
