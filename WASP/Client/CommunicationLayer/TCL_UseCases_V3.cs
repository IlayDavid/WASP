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
            posts[postID].content = content;
            return 1;
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            Dictionary<int, Moderator> mods = forums[forumID].subforums[subForumID].moderators;
            if (mods.Count > 1)
                if (mods.Remove(moderatorID))
                    return 1;
                else
                    throw new Exception("ERROR: Moderator not found");
            else
                throw new Exception("ERROR: Only one moderator left, can not removing him.");
        }

        public List<Message> getAllNotificationses(int userID, int forumID)
        {
            throw new NotImplementedException();
        }
        public List<Message> getNewNotificationses(int userID, int forumID)
        {
            throw new NotImplementedException();
        }
        public Admin addAdmin(int adminID, int forumID, int newAdminID)
        {
            forums[forumID].admins.Add(newAdminID, forums[forumID].members[newAdminID]);
            return null;
        }
        //-----------Admin Reports---------------
        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            return posts.Count(x => x.Value.containerID == subForumID);
        }

        public List<Post> postsByMember(int adminID, int forumID, int userID)
        {
            return posts.Values.SkipWhile(p => p.author.id != userID).ToList();
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        //-----------Super User Reports---------------
        public int totalForums(int userID)
        {
            return forums.Count;
        }

        public List<User> membersInDifferentForums(int userID)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
