using System;
using System.Collections.Generic;

namespace WASP
{
    public class Forum
    {
        private int id;
        private Dictionary<int, Subforum> subforums;
        private Dictionary<int, User> members;
        private Dictionary<int, User> admins;

        public Forum()
        {
            subforums = new Dictionary<int, Subforum>();
            //TODO...
        }


        internal Subforum getSubForum(int sf_ID)
        {
            Subforum tempForum;
            return subforums.TryGetValue(sf_ID,out tempForum) ? tempForum : null;
        }

        internal void addSubForum(Subforum sf)
        {
            subforums.Add(sf.id, sf);
        }

        internal bool isAdmin (int user_ID)
        {
            return admins.ContainsKey(user_ID);
        }

        internal Thread findThread(int thread_ID)
        {
            throw new NotImplementedException();
        }

        internal void definePolicy(Forum forum)
        {
            throw new NotImplementedException();
        }

        internal void subscribe(User user)
        {
            members.Add(user.id, user);
        }

        internal void update(Forum forum)
        {
            throw new NotImplementedException();
        }
    }
}