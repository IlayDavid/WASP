using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Domain
{
    class BL : IBL
    {
        SuperUser supervisor = null;
        Dictionary<int, User> users = new Dictionary<int, User>();
        Dictionary<int, Forum> forums = new Dictionary<int, Forum>();
        //Dictionary<int, Subforum> subForums = new Dictionary<int, Subforum>();
        //Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        //Dictionary<int, Post> posts = new Dictionary<int, Subforum>();


        public string addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
            User moderator = users[moderator_ID];
            if (moderator == null)
                return "user not found";

            Subforum sf = findSubForum(sf_ID);
            if(sf != null)
            {
                sf.addModerator(moderator);
                return "moderator added!";
            }
            else
                return "moderator not found";
        }

        public string confirmEmail(int user_ID)
        {
            throw new NotImplementedException();
        }

        public string createForum(Forum forum)
        {
            forums.Add(forum.id, forum);
            return "";
        }

        public string createPost(int user_ID, int thread_ID, Post post)
        {
            Thread t = findThread(thread_ID);
            if (t != null)
            {
                t.addPost(post);
                return "Post created successfully!";
            }
            else
                return "thread not found";
        }
        
        public string createSubForum(int user_ID, Subforum sf)
        {
            Forum forum = sf.forum;
            if (forum.isManager(user_ID))
            {
                forum.addSubForum(sf);
                return "sub forum added successfully!";
            }
                
            else
                return "Only forum manager can add suc forum";
        }

        public string createThread(int user_ID, int sf_ID, Thread thread)
        {
            throw new NotImplementedException();
        }

        public string defineForumPolicy(int user_ID, Forum forum)
        {
            throw new NotImplementedException();
        }

        public string deletePost(int user_ID, int thread_ID, Post post)
        {
            throw new NotImplementedException();
        }

        public string getForum(int user_ID, int forum_ID)
        {
            throw new NotImplementedException();
        }

        public string getSubForum(int user_ID, int sf_ID)
        {
            Subforum sf = findSubForum(sf_ID);
            if (sf != null)
                return "forum found";
            else
                return "forum not found";
        }

        public string getThread(int user_ID, int thread_ID)
        {
            throw new NotImplementedException();
        }

        public string initialize()
        {
            throw new NotImplementedException();
        }




        //*********************************************************

        private Subforum findSubForum(int sf_ID)
        {
            foreach (KeyValuePair<int, Forum> forum in forums)
            {
                Subforum tmp = forum.Value.getSubForum(sf_ID);
                if(tmp != null)
                    return tmp;
            }
            return null;
        }

        private Thread findThread(int thread_ID)
        {
            //get the forum in which the thread belong.
            foreach (KeyValuePair<int, Forum> forum in forums)
            {
                Thread tmp = forum.Value.findThread(thread_ID);
                if (tmp != null)
                    return tmp;
            }
            return null;
        }
    }
}
