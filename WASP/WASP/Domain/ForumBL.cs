using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses;

namespace WASP.Domain
{
    //TODO: change every instance of number '-1' to a correct number
    //TODO: decide if we look at/for users by name (string) or by id (int). shouldn't be mixed!
    class ForumBL : ForumIBL
    {
        public Forum forum { get; set; }
        
        public ForumBL(Forum newForum)
        {
            this.forum = newForum;
        }


        public List<Member> getAdmins(User user)
        {
            return forum.GetAdmins().ToList();
        }

        public List<Member> getMembers()
        {
            return forum.GetMembers().ToList();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            return forum.GetSubForum().ToList();
        }

        public Post getThread(Member member, int threadId)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(Member member, int subforumId)
        {
            throw new NotImplementedException();
        }

        public Post createThread(Member author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
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

        public Subforum createSubForum(Member member, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Post createReplyPost(Member author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            throw new NotImplementedException();
        }

        public Forum getForum()
        {
            throw new NotImplementedException();
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Member subscribeToForum(string userName, string name, string email, string pass)
        {
            throw new NotImplementedException();
        }

        public int sendMessage(Member member, Member targetMember, Message message)
        {
            throw new NotImplementedException();
        }

        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            throw new NotImplementedException();
        }

        public int confirmEmail(Member member)
        {
            throw new NotImplementedException();
        }

        public int deletePost(Member member, Post post)
        {
            throw new NotImplementedException();
        }

        public Member login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
