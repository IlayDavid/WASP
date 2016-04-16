using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class ForumDAL : ForumIDAL
    {
        Forum _forum;
        public ForumDAL(Forum forum)
        {
            _forum = forum;
        }
        public int AddMessage(Message m)
        {
            throw new NotImplementedException();
        }

        public int AddMessage(Message m, Member member)
        {
            member.AddMessage(m);
            return 1;
        }

        public int AddPost(Post p)
        {
            p.Container.AddThread(p);
            return 1;
        }

        public int AddSubforum(Subforum sf)
        {
            _forum.AddSubForum(sf);
            return 1;
        }

        public int AddMember(Member m)
        {
            m.MemberForum.AddMember(m);
            return 1;
        }

        public int DeleteMessage(Member member, int messageID)
        {
            member.DeleteMessage(messageID);
            return 1;
        }

        public int DeletePost(int postID, Subforum sf)
        {
            sf.RemoveThread(sf.GetThread(postID));
            return 1;
        }

        public int DeleteSubforum(int subforumID)
        {
            _forum.RemoveSubForum(_forum.GetSubForum(subforumID));
            return 1;
        }

        public int DeleteUser(string username)
        {
            _forum.RemoveMember(_forum.GetMember(username));
            return 1;
        }

        public Forum GetForum()
        {
            return _forum;
        }

        public Message GetMessage(Member member, int messageID)
        {
            return member.GetMessage(messageID);
        }

        public Post GetPost(int postID, Subforum sf)
        {
            return sf.GetThread(postID);
        }

        public Subforum GetSubforum(int subforumID)
        {
            return _forum.GetSubForum(subforumID);
        }

        public Post GetThread(int postID, Subforum sf)
        {
            return sf.GetThread(postID);
        }

        public Member GetUser(string username)
        {
            return _forum.GetMember(username);
        }
    }
}
