using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;

namespace AccTests
{
    class ProxyBridge : WASPBridge
    {
        public RealBridge proj;

        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            return -1;
        }

        public int confirmEmail(Member member)
        {
            return -1;
        }

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass)
        {
            return null;
        }

        public Post createReplyPost(Member Author, string content, DateTime now, Post inReplyTo)
        {
            return null;
        }

        public Subforum createSubForum(Member member, string name, string description, Member moderator, DateTime term)
        {
            return null;
        }

        public Post createThread(Member author, string title, string content, DateTime now, Subforum container)
        {
            return null;
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            return -1;
        }

        public int deletePost(Member member, Post post)
        {
            return -1;
        }

        public Member getAdmin(User user, Forum forum, string userName)
        {
            return null;
        }

        public List<Member> getAdmins(User member, Forum forum)
        {
            return null;
        }

        public List<Forum> getAllForums(User member)
        {
            return null;
        }

        public Forum getForum(Member member, int forumId)
        {
            return null;
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            return null;
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            return null;
        }

        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            return DateTime.Now.AddDays(-100);
        }

        public Subforum getSubforum(Member member, int subforumId)
        {
            return null;
        }

        public List<Subforum> getSubforums(Member member, Forum forum)
        {
            return null;
        }

        public Post getThread(Member member, int threadId)
        {
            return null;
        }

        public SuperUser initialize(string name, string userName, string email, string pass)
        {
            return null;
        }

        public Member login(string userName, string password, Forum forum)
        {
            return null;
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            return -1;
        }

        public Member subscribeToForum(string userName, string name, string email, string pass, Forum targetForum)
        {
            return null;
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            return -1;
        }
    }
}
