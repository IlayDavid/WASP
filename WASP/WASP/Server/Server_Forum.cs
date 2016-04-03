using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Server
{
    partial class Server
    {
        public List<User> getAdmins(int forumId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
                return null;
            }
        }
        public Forum getForum(int userId, int forumId)
        {
            try
            {
                return bl.getForum(userId, forumId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int createForum(string userName, Forum forum)
        {
            try
            {
                return bl.createForum(userName, forum);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Forum> getAllForums()
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
                return null;
            }
        }

        public List<User> getModerators(int subforumId)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
                return null;
            }
        }
        public Subforum getSubforum(int userId, int subforumId)
        {
            try
            {
                return bl.getSubforum(userId, subforumId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<User> getMembers(int forumId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
                return null;
            }
        }


        public List<Subforum> getSubforums(int forumId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
                return null;
            }
        }


        public int updateForum(int userId, int forumId)
        {
            try
            {
                return bl.updateForum(userId, forumId);

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int defineForumPolicy(int userId, Forum forum)
        {
            try
            {
                return bl.defineForumPolicy(userId, forum);
            }
            catch (Exception)
            {
                return -1;       
            }
        }
        public int createSubForum(string userName, int forumId, Subforum sf)
        {
            try
            {
                return bl.createSubForum(userName, forumId, sf);
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
