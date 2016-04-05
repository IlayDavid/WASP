using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataAccess;
using WASP.DataClasses;

namespace WASP.Domain
{
    class ForumBL : ForumIBL
    {
        private IDAL _dal = null;
        public int ForumID { get; set; }
        
        public ForumBL(Forum newForum, IDAL dal)
        {
            _dal = dal;
            _dal.AddForum(newForum);
            ForumID = newForum.Id;
        }

        public List<Member> getAdmins(User user)
        {
            return getForum().GetAdmins().ToList();
        }

        public List<Member> getMembers(Member member)
        {
            return getForum().GetMembers().ToList();
        }

        public List<Subforum> getSubforums(int forumId)
        {
            return getForum().GetSubForum().ToList();
        }

        public Post getThread(Member member, int threadId)
        {
            return _dal.GetPost(threadId);
        }
        //TODO: rename the function Forum.getsubforum() to Forum.getsubforums()
        public Subforum getSubforum(Member member, int subforumId)
        {
            return _dal.GetSubforum(subforumId);
        }
        
        public Post createThread(Member author, string title, string content, DateTime now, Subforum container)
        {
            var post= new Post(title, content, author, now, container);
            _dal.AddPost(post);
            return post;
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

        public Subforum createSubForum(Member member, string name, string description, Member moderator, DateTime term)
        {
            //TODO: policy
            var subforum=new Subforum(name, description, moderator, term);
            _dal.AddSubforum(subforum);
            return subforum;
        }

        public Post createReplyPost(Member author, string content, DateTime now, Post inReplyTo)
        {
            var post=new Post(content, author, now, inReplyTo);
            _dal.AddPost(post);
            return post;
        }

        public int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term)
        {
            subforum.RemoveModerator(moderator);            
            subforum.AddModerator(moderator, term);
            return 1;
        }

        public Forum getForum()
        {
            return _dal.GetForum(ForumID);
        }

        //TODO: implement according to Eden's impelementation
        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Member subscribeToForum(string userName, string name, string email, string pass)
        {
            return new Member(userName,name,email, pass, this.getForum());
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
            foreach (var reply in post.GetAllReplies())
            {
                deletePost(member, reply);
            }
            _dal.DeletePost(post.Id);
            return 1;
        }

        public Member login(string userName, string password)
        {
            var user=_dal.GetUser(userName);
            if (user.Password.Equals(password))
            {
                return (Member)user;
            }
            return null;
        }

        public List<Member> getMembers(Member member, Forum forum)
        {
            return forum.GetMembers();
        }

        public List<Subforum> getSubforums(Member member)
        {
            return getForum().GetSubForum();
        }
    }
}
