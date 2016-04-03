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
        private IBL bl = new BL();

        
        public int deletePost(string userName, int threadId, int postId)
        {
            try
            {
                return bl.deletePost(userName, threadId, postId);
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
                return bl.getThread(userId, threadId);

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
                return bl.createThread(userName, subforumId, thread);

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
                return bl.createPost(userName, threadId, post);
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
                return bl.initialize();
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
