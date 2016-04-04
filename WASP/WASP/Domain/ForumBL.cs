using System;
using System.Collections.Generic;
using System.Linq;

namespace WASP.Domain
{
    //TODO: change every instance of number '-1' to a correct number
    //TODO: decide if we look at/for users by name (string) or by id (int). shouldn't be mixed!
    class ForumBL : ForumIBL
    {
        public Forum forum { get; set; }
        Dictionary<int, Member> members;
        
        public ForumBL(Forum newForum)
        {
            this.forum = newForum;
        }

        public int addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term)
        {
                Member moderator = members[moderator_ID];
                
                Subforum sf = findSubForum(sf_ID);
                if (sf != null)
                {
                    sf.AddModerator(moderator, term);
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

        public int defineForumPolicy(int user_ID, Forum forum)
        {
            Forum f = forums[forum.Id];
            f.DefinePolicy(forum);
            return 0;
        }

        public Subforum getSubforum(int user_ID, int sf_ID)
        {
            return findSubForum(sf_ID);
        }

        public int createThread(string userName, int subforumId, Thread thread)
        {
            return createThread(-1, subforumId, thread);
        }

        //*********************************************************

        private Subforum findSubForum(int sf_ID)
        {
            return forum.GetSubForum(sf_ID);
        }

        public string subscribeToForum(Member member, int forum_ID)
        {
            try
            {
                forum.AddMember(member);
                return "Member subscribe";
            }
            catch
            {
                return "Member did not subscribe";
            }
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

        public DateTime getModeratorTermTime(Member member, int subforumId)
        {
            return getSubforum(-1, subforumId).GetModerator(member).Item2;
        }

        public int createSubForum(string userName, int forumId, Subforum sf)
        {
            getForum(-1, forumId).AddSubForum(sf);
            return 1;
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

        public int updateModeratorTerm(Member member1, Member member2, int sfId, DateTime term)
        {
            var sf = getSubforum(-1, sfId);
            if (sf == null)
                return -1;
            sf.RemoveModerator(member2);
            sf.AddModerator(member2, term);
            return 1;
        }

        public int updateForum(int userId, int forumId)
        {
                forum.Update();
                return 1;
        }

        public int sendMessage(Member userSend, Member userAcc, Message message)
        {
                if (message.isEmpty())
                    return -1;

                userAcc.sendMessage(message);
                return 1;
        }

        public int addModerator(string userId, string userId1, int sfId, DateTime term)
        {
            return getSubforum(-1,sfId).AddModerator(members[userId1],term);
        }

        public void confirmEmail(int userId)
        {
            members[userId].confirmEmail();
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
            foreach (var user in members.Values)
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
            return forum.GetAdmins().ToList();
        }

        public List<Member> getMembers(int forumId)
        {
            return forum.GetMembers().ToList();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            return forum.GetSubForum().ToList();
        }
    }
}
