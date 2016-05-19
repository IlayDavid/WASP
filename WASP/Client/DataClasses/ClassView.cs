using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataClasses
{
    public class ForumView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static List<ForumView> getView(List<Forum> forums)
        {
            List<ForumView> ret = new List<ForumView>();
            foreach (Forum f in forums)
            {
                ret.Add(new ForumView() { ID = f.id, Name = f.Name, Description = f.Description });
            }
            return ret;
        }
    }

    public class UserView
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public static List<UserView> getView(List<User> users)
        {
            List<UserView> ret = new List<UserView>();
            foreach (User u in users)
            {
                ret.Add(new UserView() { Name = u.name, UserName = u.userName});
            }
            return ret;
        }
        public static List<UserView> getView(Dictionary<int, User> users)
        {
            List<UserView> ret = new List<UserView>();
            foreach (User u in users.Values)
            {
                ret.Add(new UserView() {Name = u.name, UserName = u.userName});
            }
            return ret;
        }
    }

    public class ModeratorView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Term { get; set; }
        public string AppointBy { get; set; }

        public static List<ModeratorView> getView(List<Moderator> moderators)
        {
            List<ModeratorView> ret = new List<ModeratorView>();
            foreach (Moderator m in moderators)
            {
                ret.Add(new ModeratorView() { ID = m.user.id, Name = m.user.name,
                    UserName = m.user.userName, Email = m.user.email,
                    Term = m.term, AppointBy = m.appointBy.userName});
            }
            return ret;
        }
    }

    public class SubForumView
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PostView
    {
        public string _title { get; set; }
        public string _content { get; set; }
        public User _author { get; set; }
        public DateTime _publishedAt { get; set; }
        public DateTime _editAt { get; set; }
    }

    public class MessageView
    {
        public string _content { get; set; }
        public string _title { get; set; }
    }
    
}
