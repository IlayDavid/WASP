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


        public int addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
            try
            {
                User moderator = users[moderator_ID];
                
                Subforum sf = findSubForum(sf_ID);
                if (sf != null)
                {
                    sf.AddModerator(moderator, term);
                    return 0;
                }
                else
                    return -1;
            }
            catch { return -1; }
        }

        public string confirmEmail(int user_ID)
        {
            throw new NotImplementedException();
        }

        public int createForum(Forum forum)
        {
            forums.Add(forum.id, forum);
            return 0;
        }

        public int createPost(int user_ID, int thread_ID, Post post)
        {
            Thread t = findThread(thread_ID);
            if (t != null)
            {
                t.addPost(post);
                return 0;
            }
            else
                return -1;
        }
        
        public int createSubForum(int user_ID, Subforum sf)
        {
            Forum forum = null;//= sf.forum;
            if (forum.IsAdmin(user_ID))
            {
                forum.AddSubForum(sf);
                return 0;
            }        
            else
                return -1;
        }

        public int createThread(int user_ID, int sf_ID, Thread thread)
        {
            Subforum sf = findSubForum(sf_ID);
            if (sf != null)
            {
                //sf.AddThread(thread);
                return 0;
            }
            else
                return -1;
        }

        public void defineForumPolicy(int user_ID, Forum forum)
        {
            try
            {
                Forum f = forums[forum.Id];
                f.DefinePolicy(forum);
                //return 0;
            }
            catch (KeyNotFoundException)
            {
                //return -1; //"forum not found";
            }
            catch(Exception)
            {
                //return -1; //"cannot change policy!";
            }
        }

        public int deletePost(int user_ID, int thread_ID, int post_ID)
        {
            Thread t = findThread(thread_ID);
            if (t != null)
            {
                t.deletePost(post_ID);
                return 0;
            }
            else
                return -1;
        }

        public Forum getForum(int user_ID, int forum_ID)
        {
            try
            {
                Forum retF = forums[forum_ID];
                return retF;
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

        public Subforum getSubForum(int user_ID, int sf_ID)
        {
            return findSubForum(sf_ID);
        }

        public Thread getThread(int user_ID, int thread_ID)
        {
            return findThread(thread_ID);
        }

        public User initialize()
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
                return supervisor;
            }
            return null;
        }


        //*********************************************************

        private Subforum findSubForum(int sf_ID)
        {
            foreach (KeyValuePair<int, Forum> forum in forums)
            {
                Subforum tmp = forum.Value.GetSubForum(sf_ID);
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
                Thread tmp = forum.Value.FindThread(thread_ID);
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
                //to.sendMessage(message);
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
                f.AddMember(user);
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
                Forum f = forums[forum.Id];
                f.Update(forum);
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

        public Subforum getSubforum(int userId, int subforumId)
        {
            throw new NotImplementedException();
        }

        public int createThread(string userName, int subforumId, Thread thread)
        {
            throw new NotImplementedException();
        }

        public int createForum(string userName, Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<User> getModerators(int subforumId)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(string userName, int subforumId)
        {
            throw new NotImplementedException();
        }

        public int createSubForum(string userName, int forumId, Subforum sf)
        {
            throw new NotImplementedException();
        }

        public List<Forum> getAllForums()
        {
            throw new NotImplementedException();
        }

        public int createPost(string userName, int threadId, Post post)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term)
        {
            throw new NotImplementedException();
        }

        public int updateForum(int userId, int forumId)
        {
            throw new NotImplementedException();
        }

        int IBL.defineForumPolicy(int userId, Forum forum)
        {
            throw new NotImplementedException();
        }

        public int sendMessage(string userSend, string userAcc, Message message)
        {
            throw new NotImplementedException();
        }

        public int addModerator(string userId, string userId1, int sfId, DateTime term)
        {
            throw new NotImplementedException();
        }

        void IBL.confirmEmail(int userId)
        {
            throw new NotImplementedException();
        }

        public int deletePost(string userName, int threadId, int postId)
        {
            throw new NotImplementedException();
        }

        public int login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<User> getAdmins(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            throw new NotImplementedException();
        }
    }
}
