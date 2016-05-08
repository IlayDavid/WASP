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
        public Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {
            Subforum sf = forums[forumID].subforums[subForumID];
            User admin = forums[forumID].members[userID];
            User moderator = forums[forumID].members[moderatorID];
            Moderator m = new Moderator(moderator, term, admin);
            sf.moderators.Add(m.user.id, m);
            return m;
        }

        public int confirmEmail(int code)
        {
            throw new NotImplementedException();
        }

        public int deletePost(int postID)
        {
            if (posts[postID].inReplyTo != null)
            {
                List<Post> replies = posts[postID].inReplyTo.replies;
                replies.Remove(replies.First(x => x.id == postID));
            }
            posts.Remove(postID);
            
            return 1;
        }

        public int sendMessage(int targetUserID, string message)
        {
            throw new NotImplementedException();
        }

        public int updateModeratorTerm(int subforumID, int moderatorID, DateTime term)
        {
            subforums[subforumID].moderators[moderatorID].term = term;
            return 1;
        }
    }
}
