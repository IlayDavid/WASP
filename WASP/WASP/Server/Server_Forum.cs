using System;
using System.Collections.Generic;
using WASP.DataClasses;

namespace WASP.Server
{
    partial class Server
    {
        public List<Member> getAdmins(User member, Forum forum)
        {
            try
            {
                return _bl.getAdmins(forumId);
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
                return _bl.getForum(userId, forumId);
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
                return _bl.createForum(userName, forum);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Forum> getAllForums(User member)
        {
            try
            {
                return _bl.getAllForums();
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
                return _bl.getModerators(subforumId);
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
                return _bl.getSubforum(userId, subforumId);
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
                return _bl.getMembers(forumId);
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
                return _bl.getSubforums(forumId);
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
                return _bl.defineForumPolicy(userId, forum);
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
                return _bl.createSubForum(userName, forumId, sf);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}