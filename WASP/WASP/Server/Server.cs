using System;
using System.Collections.Generic;
using System.Net;
using WASP.Domain;

namespace WASP.Server
{
    // partial class of Server.
    // in charge of posts and other functions
    // server_user has explicit user methods
    // server_forum has explicit forum methods
    partial class Server : ServerAPI
    {
        private IBL _bl = new BL();

        
        public int deletePost(string userName, int threadId, int postId)
        {
            try
            {
                return _bl.deletePost(userName, threadId, postId);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Thread getThread(int userId, int threadId)
        {
            try
            {
                return _bl.getThread(userId, threadId);

            }
            catch (Exception)
            {
                return null;
            }
        }

   

        public int createThread(string userName, int subforumId, Thread thread)
        {
            try
            {
                return _bl.createThread(userName, subforumId, thread);

            }
            catch (Exception)
            {
                return -1;
            }
        }

        

        
        
        public int createPost(string userName, int threadId, Post post)
        {
            try
            {
                return _bl.createPost(userName, threadId, post);
            }
            catch (Exception)
            {
                return -1;                
            }
        }

        
        public User initialize()
        {
            try
            {
                return _bl.initialize();
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
