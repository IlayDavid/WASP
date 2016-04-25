using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    class DALSQL : DAL
    {
        public Admin CreateAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Forum CreateForum(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Moderator CreateModerator(Moderator mod)
        {
            throw new NotImplementedException();
        }

        public Subforum CreateSubForum(Subforum sf)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Admin[] GetAdmins(Collection<int> adminsIds)
        {
            throw new NotImplementedException();
        }

        public Forum[] GetForums(Collection<int> forumsIds)
        {
            throw new NotImplementedException();
        }

        public Moderator[] GetModerators(Collection<int> moderatorIds, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Subforum[] GetSubForums(Collection<int> subForumIds, Forum forum)
        {
            throw new NotImplementedException();
        }

        public User[] GetUseres(Collection<int> userIds, Forum forum)
        {
            throw new NotImplementedException();
        }

        public Admin UpdateAdmin(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Forum UpdateForum(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Moderator updateModerator(Moderator mod)
        {
            throw new NotImplementedException();
        }

        public Subforum UpdateSubForum(Subforum sf)
        {
            throw new NotImplementedException();
        }

        public User updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
