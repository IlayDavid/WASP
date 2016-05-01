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
        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            if (userID < 0 || forumID < 0 || moderatorID < 0 || subForumID < 0) throw new Exception("ERROR: ID is illegal");
            DateTime now = DateTime.Now.Date;
            if (term.Date.CompareTo(now) <= 0) throw new Exception("ERROR: Term should be after " + now);

            return _cl.addModerator(userID, forumID, moderatorID, subForumID, term);
        }

        public int confirmEmail(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            throw new NotImplementedException();
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            throw new NotImplementedException();
        }
    }
}
