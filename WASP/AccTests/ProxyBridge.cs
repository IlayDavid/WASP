using System;
using System.Collections.Generic;
using WASP;
using WASP.DataClasses;

namespace AccTests
{
    class ProxyBridge : WASPBridge
    {
        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            throw new NotImplementedException();
        }

        public int confirmEmail(Member member)
        {
            throw new NotImplementedException();
        }

        public Forum createForum(SuperUser creator, string forumName, string description, string userName, string adminName, string email, string pass)
        {
            throw new NotImplementedException();
        }

        public Post createReplyPost(Member Author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            throw new NotImplementedException();
        }

        public Subforum createSubForum(Member member, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Post createThread(Member author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            throw new NotImplementedException();
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public int deletePost(Member member, Post post)
        {
            throw new NotImplementedException();
        }

        public List<Member> getAdmins(User member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<Forum> getAllForums(User member)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(Member member, int forumId)
        {
            throw new NotImplementedException();
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(Member member, int subforumId)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(Member member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Post getThread(Member member, int threadId)
        {
            throw new NotImplementedException();
        }

        public SuperUser initialize(string name, string userName, string email, string pass)
        {
            throw new NotImplementedException();
        }

        public Member login(string userName, string password, Forum forum)
        {
            throw new NotImplementedException();
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            throw new NotImplementedException();
        }

        public Member subscribeToForum(string userName, string name, string email, string pass, Forum targetForum)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            throw new NotImplementedException();
        }
    }
}
