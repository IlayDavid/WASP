using System;
using System.Collections.Generic;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace AccTests
{
    class ProxyBridge : WASPBridge
    {
        private RealBridge proj;

        public ProxyBridge(RealBridge bridge)
        {
            proj = bridge;
        }

        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            if (proj != null)
                return proj.addModerator(member, moderator, subforum, term);
            return -1;
        }

        public int confirmEmail(Member member)
        {
            if (proj != null)
                return proj.confirmEmail(member);
            return -1;
        }

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass, Policy policy)
        {
            if (proj != null)
                return proj.createForum(creator, forumName, description, userName, adminName, email, pass, policy);
            return null;
        }

        public Post createReplyPost(Member Author, string content, DateTime now, Post inReplyTo)
        {
            if (proj != null)
                return proj.createReplyPost(Author, content, now, inReplyTo);
            return null;
        }

        public Subforum createSubForum(Member member, string name, string description, Member moderator, DateTime term)
        {
            if (proj != null)
                return proj.createSubForum(member, name, description, moderator, term);
            return null;
        }

        public Post createThread(Member author, string title, string content, DateTime now, Subforum container)
        {
            if (proj != null)
                return proj.createThread(author, title, content, now, container);
            return null;
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            if (proj != null)
                return proj.defineForumPolicy(member, forum);
            return -1;
        }

        public int deletePost(Member member, Post post)
        {
            if (proj != null)
                return proj.deletePost(member, post);
            return -1;
        }

        public Member getAdmin(User user, Forum forum, string userName)
        {
            if (proj != null)
                return proj.getAdmin(user, forum, userName);
            return null;
        }

        public SuperUser login(string userName, string password)
        {
            return proj.login(userName, password);
        }

        public List<Member> getAdmins(User member, Forum forum)
        {
            if (proj != null)
                return proj.getAdmins(member, forum);
            return null;
        }

        public List<Forum> getAllForums(User member)
        {
            if (proj != null)
                return proj.getAllForums(member);
            return null;
        }

        public Forum getForum(Member member, int forumId)
        {
            if (proj != null)
                return proj.getForum(member, forumId);
            return null;
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            if (proj != null)
                return proj.getMembers(member, forum);
            return null;
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            if (proj != null)
                return proj.getModerators(member, subforum);
            return null;
        }

        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            if (proj != null)
                return proj.getModeratorTermTime(member, moderator, subforum);
            return DateTime.Now.AddDays(-100);
        }

        public Subforum getSubforum(Member member, int subforumId)
        {
            if (proj != null)
                return proj.getSubforum(member, subforumId);
            return null;
        }

        public List<Subforum> getSubforums(Member member, Forum forum)
        {
            if (proj != null)
                return proj.getSubforums(member, forum);
            return null;
        }

        public Post getThread(Member member, int threadId)
        {
            if (proj != null)
                return proj.getThread(member, threadId);
            return null;
        }

        public SuperUser initialize(string name, string userName, string email, string pass)
        {
            if (proj != null)
                return proj.initialize(name, userName, email, pass);
            return null;
        }

        public Member login(string userName, string password, Forum forum)
        {
            if (proj != null)
                return proj.login(userName, password, forum);
            return null;
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            if (proj != null)
                return proj.sendMessage(member, targetMember, message);
            return -1;
        }

        public Member subscribeToForum(string userName, string name, string email, string pass, Forum targetForum)
        {
            if (proj != null)
                return proj.subscribeToForum(userName, name, email, pass, targetForum);
            return null;
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            if (proj != null)
                return proj.updateModeratorTerm(member, moderator, subforum, term);
            return -1;
        }
    }
}
