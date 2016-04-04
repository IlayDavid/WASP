using System;
using System.Collections.Generic;
using System.Linq;

namespace WASP.Domain
{
    //TODO: change every instance of number '-1' to a correct number
    //TODO: decide if we look at/for users by name (string) or by id (int). shouldn't be mixed!
    class BL : IBL
    {
        private static bool _initialized = false;
        private static SuperUser supervisor = null;
        Dictionary<int, Member> users;


        public int addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
                Member moderator = users[moderator_ID];
                
                Subforum sf = findSubForum(sf_ID);
                if (sf != null)
                {
                    sf.AddModerator(moderator, term);
                    return 0;
                }
                else
                    return 11111;
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

        public int defineForumPolicy(int user_ID, Forum forum)
        {
                Forum f = forums[forum.Id];
                f.DefinePolicy(forum);
                return 0;
        }

        public Forum getForum(int user_ID, int forum_ID)
        {
                Forum retF = forums[forum_ID];
                return retF;
        }

        public Subforum getSubforum(int user_ID, int sf_ID)
        {
            return findSubForum(sf_ID);
        }

        public int createThread(string userName, int subforumId, Thread thread)
        {
            return createThread(-1, subforumId, thread);
        }

        public Thread getThread(int user_ID, int thread_ID)
        {
            return findThread(thread_ID);
        }

        public static SuperUser initialize()
        {
            if (!_initialized)
            {
                const string SUPERUSERNAME = "admin";
                const string SUPERPASSWORD = "wasp1234Sting";
                supervisor = new SuperUser(SUPERUSERNAME, "", "", SUPERPASSWORD);
                _initialized = true;
                return supervisor;
            }
            return null;
        }


        //*********************************************************

        private Subforum findSubForum(int sf_ID)
        {
            foreach (Forum forum in forums.Values)
            {
                Subforum tmp = forum.GetSubForum(sf_ID);
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

        public string subscribeToForum(Member member, int forum_ID)
        {
            try
            {
                Forum f = forums[forum_ID];
                f.AddMember(member);
                return "Member subscribe";
            }
            catch
            {
                return "Member did not subscribe";
            }
        }


        public int createForum(string userName, Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<Member> getModerators(int subforumId)
        {
            var tuples= getSubforum(-1, subforumId).GetModerators();
            List<Member> mods=new List<Member>();
            foreach (var tuple in tuples)
            {
                mods.Add(tuple.Item1);
            }
            return mods;
        }

        public DateTime getModeratorTermTime(string userName, int subforumId)
        {
            return getSubforum(-1, subforumId).GetModerator(userName).Item2;
        }

        public int createSubForum(string userName, int forumId, Subforum sf)
        {
            getForum(-1, forumId).AddSubForum(sf);
            return 1;
        }

        public List<Forum> getAllForums()
        {
            return forums.Values.ToList();
        }

        public int createPost(string userName, int threadId, Post post)
        {
            Thread t = findThread(threadId);
            if (t != null)
            {
                t.addPost(post);
                return 0;
            }
            else
                return -1;
        }

        public int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term)
        {
            var sf = getSubforum(-1, sfId);
            if (sf == null)
                return -1;
            sf.RemoveModerator(userName2);
            sf.AddModerator(userName2,term);
            return 1;
        }

        public int updateForum(int userId, int forumId)
        {
                Forum f = forums[forumId];
                f.Update();
                return 1;
        }

        public int sendMessage(string userSend, string userAcc, Message message)
        {
                if (message.isEmpty())
                    return -1;

                Member to = users[message.to_ID];
                to.sendMessage(message);
                return 1;
        }

        public int addModerator(string userId, string userId1, int sfId, DateTime term)
        {
            return getSubforum(-1,sfId).AddModerator(users[userId1],term);
        }

        public void confirmEmail(int userId)
        {
            users[userId].confirmEmail();
        }

        public int deletePost(string userName, int threadId, int postId)
        {
            Thread t = findThread(threadId);
            if (t != null)
            {
                t.deletePost(postId);
                return 0;
            }
            else
                return -1;
        }

        public int login(string userName, string password)
        {
            foreach (var user in users.Values)
            {
                if (user.UserName.Equals(userName) && user.Password.Equals(password))
                {
                    throw new NotImplementedException();
                }
            }
            return -1;
        }

        public List<Member> getAdmins(int forumId)
        {
            return forums[forumId].GetAdmins().ToList();
        }

        public List<Member> getMembers(int forumId)
        {
            return forums[forumId].GetMembers().ToList();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            return forums[forumId].GetSubForum().ToList();
        }
    }
}
