using System;

namespace WASP.Server
{
    class Server : ServerAPI
    {
        private bool _initialized=false;
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
            if (!_initialized)
            {
                const string SUPERUSERNAME = "admin";
                const string SUPERPASSWORD = "wasp1234Sting";
                var superUser = SuperUser.CreateSuperUser();
                superUser.Password = SUPERPASSWORD;
                superUser.Username = SUPERUSERNAME;
                _initialized = true;
                return "system initialized";
            }
            return "already initialized. action failed.";
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
