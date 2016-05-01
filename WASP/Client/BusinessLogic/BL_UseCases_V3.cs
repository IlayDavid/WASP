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
        //---------------------------Version 3 Use Cases Start------------------------------------
        public int editPost(int userID, int forumID, int postID, string content)
        {
            throw new NotImplementedException();
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            if (userID < 0 || forumID < 0 || subForumID < 0 || moderatorID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.deleteModerator(userID, forumID, moderatorID, subForumID);
        }

        //-----------Admin Reports---------------
        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            throw new NotImplementedException();
        }

        public int memberTotalMessages(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        //-----------Super User Reports---------------
        public int totalForums(int userID)
        {
            throw new NotImplementedException();
        }

        public List<User> membersInDifferentForums(int userID)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
