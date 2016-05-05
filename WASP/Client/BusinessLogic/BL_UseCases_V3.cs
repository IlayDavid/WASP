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
        public int editPost(int postID, string content)
        {
            if (postID < 0) throw new Exception("ERROR: ID is illegal");
            if(!IsStrValid(content)) throw new Exception("ERROR: content is empty");

            return _cl.editPost(postID, content);
        }

        public Admin addAdmin(int newAdminID)
        {
            if (newAdminID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.addAdmin(newAdminID);
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            if (subForumID < 0 || moderatorID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.deleteModerator(moderatorID, subForumID);
        }

        public List<Message> getAllNotificationses()
        {
            throw new NotImplementedException();
        }
        public List<Message> getNewNotificationses()
        {
            throw new NotImplementedException();
        }
        //-----------Admin Reports---------------
        public int subForumTotalMessages(int subForumID)
        {
            if (subForumID < 0) throw new Exception("ERROR: ID is illegal");
            return _cl.subForumTotalMessages(subForumID);
        }

        public List<Post> postsByMember(int userID)
        {
            if (userID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.postsByMember(userID);
        }

        public ModeratorReport moderatorReport()
        {
            return _cl.moderatorReport();
        }

        //-----------Super User Reports---------------
        public int totalForums()
        {
            return _cl.totalForums();
        }

        public List<User> membersInDifferentForums()
        {
            return _cl.membersInDifferentForums();
        }
        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
