using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        //---------------------------Version 3 Use Cases Start------------------------------------
        public int editPost(int userID, int forumID, int postID, string content)
        {
            throw new NotImplementedException();
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            Dictionary<int, Moderator> mods = forums[forumID].subforums[subForumID]._moderators;
            if (mods.Count > 1)
                if (mods.Remove(moderatorID))
                    return 1;
                else
                    throw new Exception("ERROR: Moderator not found");
            else
                throw new Exception("ERROR: Only one moderator left, can not removing him.");
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
