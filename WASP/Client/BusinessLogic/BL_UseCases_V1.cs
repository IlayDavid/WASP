using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public partial class BL : IBL
    {
        //---------------------------Version 1 Use Cases Start------------------------------------
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            return null;
            throw new NotImplementedException();
        }
        public int isInitialize()
        {
            return 0;
            throw new NotImplementedException();
        }

        public Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            throw new NotImplementedException();
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(string userName, string name, string email, string pass, int targetForumID)
        {
            throw new NotImplementedException();
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            throw new NotImplementedException();
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            throw new NotImplementedException();
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}
