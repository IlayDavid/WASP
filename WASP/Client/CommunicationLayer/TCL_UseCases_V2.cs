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
        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            Subforum sf = forums[forumID].subforums[subForumID];
            User admin = forums[forumID].members[userID];
            User moderator = forums[forumID].members[moderatorID];
            Moderator m = new Moderator(moderator, term, admin);
            sf.moderators.Add(m.user.id, m);
            return m;
        }

        public int confirmEmail(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            return posts.Remove(postID)? 1:-1;
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            subforums[subforumID].moderators[moderatorID].term = term;
            return 1;
        }
    }
}
