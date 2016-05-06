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
        public int editPost(int postID, string content)
        {
            posts[postID].content = content;
            return 1;
        }

        public int deleteModerator(int moderatorID, int subForumID)
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

        public List<Message> getAllNotificationses()
        {
            throw new NotImplementedException();
        }
        public List<Message> getNewNotificationses()
        {
            throw new NotImplementedException();
        }
        public Admin addAdmin(int newAdminID)
        {
            forums[forumID].admins.Add(newAdminID, forums[forumID].members[newAdminID]);
            return null;
        }
        //-----------Admin Reports---------------
        public int subForumTotalMessages(int subForumID)
        {
            return posts.Count(x => x.Value.containerID == subForumID);
        }

        public List<Post> postsByMember(int userID)
        {
            return posts.Values.SkipWhile(p => p.author.id != userID).ToList();
        }

        public ModeratorReport moderatorReport()
        {
            throw new NotImplementedException();
        }

        //-----------Super User Reports---------------
        public int totalForums()
        {
            return forums.Count;
        }

        public List<User> membersInDifferentForums()
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
