using System;
using System.Collections.Generic;
using WASP.DataClasses;
using WASP.Domain;

namespace WASP.Server
{
    partial class Server
    {
        
        public List<Member> getAdmins(User user, Forum forum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(forum);
                return forum_bl.getAdmins(user);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Forum getForum(Member member, int forumId)
        {
            try
            {
                return bl.getForum(member, forumId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Forum createForum(SuperUser creator, String forumName, String description, String userName, String adminName, String email, String pass)
        {
            try
            {
                return bl.createForum(creator, forumName, description, userName, adminName, email, pass);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Forum> getAllForums(User member)
        {
            try
            {
                return bl.getAllForums();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Member> getModerators(Member member, Subforum subforum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getModerators(member, subforum);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Subforum getSubforum(Member member, int subforumId)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getSubforum(member, subforumId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Member> getMembers(Member member, Forum forum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getMembers(member, forum);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Subforum> getSubforums(Member member, Forum forum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.getSubforums(member);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int defineForumPolicy(SuperUser member, Forum forum)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(forum);
                return forum_bl.defineForumPolicy(member, forum);
            }
            catch (Exception)
            {
                return -1;       
            }
        }
        public Subforum createSubForum(Member member, String name, String description)
        {
            try
            {
                ForumIBL forum_bl = bl.getForumIBL(member.MemberForum);
                return forum_bl.createSubForum(member, name, description);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}