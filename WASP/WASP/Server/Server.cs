using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Server
{
    class Server : ServerAPI
    {
        public string addModerator(User user, User moderator, Subforum sf, DateTime term)
        {
            throw new NotImplementedException();
        }

        public string confirmEmail(User user)
        {
            throw new NotImplementedException();
        }

        public string createForum(User user, Forum forum)
        {
            throw new NotImplementedException();
        }

        public string createPost(User user, Subforum sf, Thread thread, Post post)
        {
            throw new NotImplementedException();
        }

        public string createSubForum(User user, Subforum sf)
        {
            throw new NotImplementedException();
        }

        public string initialize()
        {
            throw new NotImplementedException();
        }

        public string login(string password, string username)
        {
            throw new NotImplementedException();
        }

        public string sendMessage(User user, Message message)
        {
            throw new NotImplementedException();
        }

        public string subscribeToForum(User user, Forum forum)
        {
            throw new NotImplementedException();
        }

        public string updateForum(User user, Forum forum)
        {
            throw new NotImplementedException();
        }
    }
}
