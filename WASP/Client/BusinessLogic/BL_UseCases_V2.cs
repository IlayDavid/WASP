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
        public Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {
            if ( moderatorID < 0 || subForumID < 0) throw new Exception("ERROR: ID is illegal");
            DateTime now = DateTime.Now.Date;
            if (term.Date.CompareTo(now) <= 0) throw new Exception("ERROR: Term should be after " + now);

            return _cl.addModerator(moderatorID, subForumID, term);
        }

        public int confirmEmail(int code)
        {
            return _cl.confirmEmail(code);
        }

        public int deletePost(int postID)
        {
            return _cl.deletePost(postID);
        }

        public int sendMessage(string targetUserNameID, string message)
        {
            return _cl.sendMessage(targetUserNameID, message);
        }

        public int updateModeratorTerm(int subforumID, int moderatorID, DateTime term)
        {
            return _cl.updateModeratorTerm(moderatorID, subforumID, term);
        }
    }
}
