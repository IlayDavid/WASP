using System;
using System.Collections.Generic;
using WASP.DataClasses;
using WASP.Domain;

namespace WASP.Server
{
    partial class Server
    {
        public List<Member> getAdmins(User member, Forum forum)
        {
            try
            {
                return _bl.getAdmins(forum);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getModerators(member, subforum);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getSubforum(member, subforumId);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getMembers(member, forum);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.getSubforums(member);
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
                IBL bl = null;
                forumsBL.TryGetValue(member.MemberForum, out bl);

                return bl.createSubForum(member, name, description);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}