using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
using WASP.Server;

namespace AccTests
{
    class RealBridge : WASPBridge
    {
        private ServerAPI _serverAPI;

        public RealBridge()
        {
            _serverAPI = new Server();
        }
        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            return _serverAPI.addModerator(member, moderator, subforum, term);
        }

        public int confirmEmail(Member member)
        {
            return _serverAPI.confirmEmail(member);
        }

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass, Policy policy)
        {
            return _serverAPI.createForum(creator, forumName, description, userName, adminName, email, pass, policy);
        }

        public Post createReplyPost(Member Author, string content, DateTime now, Post inReplyTo)
        {
            return _serverAPI.createReplyPost(Author, content, now, inReplyTo);
        }

        public Subforum createSubForum(Member member, string name, string description, Member moderator, DateTime term)
        {
            return _serverAPI.createSubForum(member, name, description, moderator, term);
        }

        public Post createThread(Member author, string title, string content, DateTime now, Subforum container)
        {
            return _serverAPI.createThread(author, title, content, now, container);
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public int deletePost(Member member, Post post)
        {
            return _serverAPI.deletePost(member, post);
        }

        public Member getAdmin(User user, Forum forum, string userName)
        {
            return  _serverAPI.getAdmin(user, forum, userName);
        }

        public List<Member> getAdmins(User member, Forum forum)
        {
            return _serverAPI.getAdmins(member, forum);
        }

        public List<Forum> getAllForums(User member)
        {
            return _serverAPI.getAllForums(member);
        }

        public Forum getForum(Member member, int forumId)
        {
            return _serverAPI.getForum(member, forumId);
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            return _serverAPI.getMembers(member, forum);
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            return _serverAPI.getModerators(member, subforum);
        }

        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            return _serverAPI.getModeratorTermTime(member, moderator, subforum);
        }

        public Subforum getSubforum(Member member, int subforumId)
        {
            return _serverAPI.getSubforum(member, subforumId);
        }

        public List<Subforum> getSubforums(Member member, Forum forum)
        {
            return _serverAPI.getSubforums(member, forum);
        }

        public Post getThread(Member member, int threadId)
        {
            return _serverAPI.getThread(member, threadId);
        }

        public SuperUser initialize(string name, string userName, string email, string pass)
        {
            return _serverAPI.initialize(name, userName, email, pass);
        }

        public Member login(string userName, string password, Forum forum)
        {
            return _serverAPI.login(userName, password, forum);
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            return _serverAPI.sendMessage(member, targetMember, message);
        }

        public Member subscribeToForum(string userName, string name, string email, string pass, Forum targetForum)
        {
            return _serverAPI.subscribeToForum(userName, name, email, pass, targetForum);
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            return _serverAPI.updateModeratorTerm(member, moderator, subforum, term);
        }
    }
}
