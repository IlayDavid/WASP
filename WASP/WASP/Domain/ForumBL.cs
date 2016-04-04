using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses;

namespace WASP.Domain
{
    class ForumBL : ForumIBL
    {
        public Forum Forum { get; set; }
        
        public ForumBL(Forum newForum)
        {
            Forum = newForum;
        }

        public List<Member> getAdmins(User user)
        {
            return Forum.GetAdmins().ToList();
        }

        public List<Member> getMembers(Member member)
        {
            return Forum.GetMembers().ToList();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            return Forum.GetSubForum().ToList();
        }

        public Post getThread(Member member, int threadId)
        {
            //goes over the forums, and returns the first post whose id=threadid, or null
            return member.MemberForum.GetSubForum().Select(forum => forum.GetThreads().First((x) => x.Id == threadId)).FirstOrDefault(post => post != null);
        }
        //TODO: rename the function Forum.getsubforum() to Forum.getsubforums()
        public Subforum getSubforum(Member member, int subforumId)
        {
            return member.MemberForum.GetSubForum(subforumId);
        }

        //TODO: check api. is "inreplyto" required?
        //TODO: doublecheck current implementation
        public Post createThread(Member author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            return new Post(title, content, author, now, inReplyTo, container, editAt);
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            var tuples = subforum.GetModerators();
            return tuples.Select(tuple => tuple.Item1).ToList();
        }

        public DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum)
        {
            return subforum.GetModerator(moderator).Item2;
        }

        public Subforum createSubForum(Member member, string name, string description)
        {
            return new Subforum(name, description);
        }

        public Post createReplyPost(Member author, string title, string content, DateTime now, Post inReplyTo, Subforum container, DateTime editAt)
        {
            return new Post(title, content, author, now, inReplyTo, container, editAt);
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            subforum.RemoveModerator(moderator);            
            subforum.AddModerator(moderator, term);
            return 1;
        }

        public Forum getForum()
        {
            return Forum;
        }

        //TODO: implement according to Eden's impelementation
        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Member subscribeToForum(string userName, string name, string email, string pass)
        {
            return new Member(userName,name,email, pass, Forum);
        }

        //TODO: fix the entity to support messages
        public int sendMessage(Member member, Member targetMember, Message message)
        {
            throw new NotImplementedException();
        }

        public int addModerator(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            subforum.AddModerator(moderator,term);
            return 1;
        }

        public int confirmEmail(Member member)
        {
            member.confirmMail();
            return 1;
        }

        public int deletePost(Member member, Post post)
        {
            if (post.IsOriginal())
            {
                post.InReplyTo.RemoveReply(post);
            }
            else
            {
                post.Container.RemoveThread(post);
            }
            return 1;
        }

        public Member login(string userName, string password)
        {
            return Forum.GetMembers().First((x) => x.UserName.Equals(userName) && x.Password.Equals(password));
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            return forum.GetMembers();
        }

        public List<Subforum> getSubforums(Member member)
        {
            return Forum.GetSubForum();
        }
    }
}
