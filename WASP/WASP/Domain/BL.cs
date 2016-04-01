using System;
using System.Collections.Generic;

namespace WASP.Domain
{
    class BL : IBL
    {
        private bool _initialized = false;
        SuperUser supervisor = null;
        Dictionary<int, User> users;
        Dictionary<int, Forum> forums;


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
            Subforum sf = findSubForum(sf_ID);
            if (sf != null)
            {
                sf.addThread(thread);
                return "Thread added to subforum";
            }
            else
                return "subforum not found";
        }

        public string defineForumPolicy(int user_ID, Forum forum)
        {
            try
            {
                Forum f = forums[forum.id];
                f.definePolicy(forum);
                return "forum Policy has been defined";
            }
            catch (KeyNotFoundException)
            {
                return "forum not found";
            }
            catch(Exception)
            {
                return "cannot change policy!";
            }
        }

        public string deletePost(int user_ID, int thread_ID, int post_ID)
        {
            Thread t = findThread(thread_ID);
            if (t != null)
            {
                t.deletePost(post_ID);
                return "post deleted!";
            }
            else
                return "post not found";
        }

        public string getForum(int user_ID, int forum_ID)
        {
            try
            {
                Forum retF = forums[forum_ID];
                return "forum found - dont know what to return in the string";
            }
            catch(KeyNotFoundException)
            {
                return "forum not found";
            }

        }

        public string getSubForum(int user_ID, int sf_ID)
        {
            Subforum sf = findSubForum(sf_ID);
            if (sf != null)
                return "sub forum found";
            else
                return "sub forum not found";
        }

        public string getThread(int user_ID, int thread_ID)
        {
            Thread t = findThread(thread_ID);
            if(t != null)
                return "thread found";
            else
                return "thread not found";
        }

        public string initialize()
        {
            if (!_initialized)
            {
                users = new Dictionary<int, User>();
                forums = new Dictionary<int, Forum>();

                const string SUPERUSERNAME = "admin";
                const string SUPERPASSWORD = "wasp1234Sting";
                supervisor = SuperUser.CreateSuperUser();
                supervisor.Password = SUPERPASSWORD;
                supervisor.Username = SUPERUSERNAME;
                _initialized = true;
                return "system initialized";
            }
            return "already initialized. action failed.";
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

        public string sendMessage(int user_ID, Message message)
        {
            try
            {
                if (message.isEmpty())
                    return "message is empty";

                User to = users[message.to_ID];                
                to.sendMessage(message);
                return "message sent";
            }
            catch
            {
                return "user not found";
            }
        }

        public string subscribeToForum(User user, int forum_ID)
        {
            try
            {
                Forum f = forums[forum_ID];
                f.subscribe(user);
                return "user subscribe";
            }
            catch
            {
                return "user did not subscribe";
            }
        }

        public string updateForum(int user_ID, Forum forum)
        {
            try
            {
                Forum f = forums[forum.id];
                f.update(forum);
                return "forum updated";
            }
            catch
            {
                return "forum did not updated";
            }
        }

        public string updateModeratorTerm(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
            Subforum sf = findSubForum(sf_ID);
            if (sf != null)
            {
                sf.updateModeratorTerm(moderator_ID, term);
                return "moderator term updated";
            }
            else
                return "sub forum not found";
        }
    }
}
