using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.DataAccess
{
    class DAL : IDAL
    {
        private List<Forum> _forums;
        private List<Message> _messages;
        private List<Post> _posts;
        private List<Subforum> _subForums;
        private List<User> _users;

        public DAL()
        {
            _forums = new List<Forum>();
            _messages = new List<Message>();
            _posts = new List<Post>();
            _subForums = new List<Subforum>();
            _users = new List<User>();
        }
        public int AddForum(Forum f)
        {
            try
            {
                _forums.Add(f);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int AddMessage(Message m)
        {
            try
            {
                _messages.Add(m);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int AddPost(Post p)
        {
            try
            {
                _posts.Add(p);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int AddSubforum(Subforum sf)
        {
            try
            {
                _subForums.Add(sf);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int AddUser(User u)
        {
            try
            {
                _users.Add(u);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int DeleteForum(int forumID)
        {
            foreach(Forum f in _forums)
            {
                if (f.Id == forumID)
                {
                    _forums.Remove(f);
                    return 0;
                }
            }
            return -1;
        }

        public int DeleteMessage(int messageID)
        {
            foreach (Message m in _messages)
            {
                if (m.Id == messageID)
                {
                    _messages.Remove(m);
                    return 0;
                }
            }
            return -1;
        }

        public int DeletePost(int postID)
        {
            foreach (Post p in _posts)
            {
                if (p.Id == postID)
                {
                    _posts.Remove(p);
                    return 0;
                }
            }
            return -1;
        }

        public int DeleteSubforum(int subforumID)
        {
            foreach (Subforum sf in _subForums)
            {
                if (sf.Id == subforumID)
                {
                    _subForums.Remove(sf);
                    return 0;
                }
            }
            return -1;
        }

        public int DeleteUser(string username)
        {
            foreach (User u in _users)
            {
                if (u.UserName == username)
                {
                    _users.Remove(u);
                    return 0;
                }
            }
            return -1;
        }

        public Forum GetForum(int forumID)
        {
            foreach (Forum f in _forums)
            {
                if (f.Id == forumID)
                {
                    return f;
                }
            }
            return null;
        }

        public Message GetMessage(int messageID)
        {
            foreach (Message m in _messages)
            {
                if (m.Id == messageID)
                {
                    return m;
                }
            }
            return null;
        }

        public Post GetPost(int postID)
        {
            foreach (Post p in _posts)
            {
                if (p.Id == postID)
                {
                    return p;
                }
            }
            return null;
        }

        public Subforum GetSubforum(int subforumID)
        {
            foreach (Subforum sf in _subForums)
            {
                if (sf.Id == subforumID)
                {
                    return sf;
                }
            }
            return null;
        }

        public User GetUser(string username)
        {
            foreach (User u in _users)
            {
                if (u.UserName == username)
                {
                    return u;
                }
            }
            return null;
        }
    }
}
