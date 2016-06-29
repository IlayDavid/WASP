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
            _logger.writeToFile("edit post");
            if (postID < 0) throw new Exception("ERROR: ID is illegal");
            if(!IsStrValid(content)) throw new Exception("ERROR: content is empty");

            return _cl.editPost(postID, content);
        }

        public Admin addAdmin(int newAdminID)
        {
            _logger.writeToFile("add admin");
            if (newAdminID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.addAdmin(newAdminID);
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            _logger.writeToFile("delete moderator");
            if (subForumID < 0 || moderatorID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.deleteModerator(moderatorID, subForumID);
        }

        public List<Notification> getAllNotificationses()
        {
            _logger.writeToFile("get all notifications");
            return _cl.getAllNotificationses();
        }
        public List<Notification> getNewNotificationses()
        {
            _logger.writeToFile("get new notifications");
            return _cl.getNewNotificationses();
        }
        //-----------Admin Reports---------------
        public int subForumTotalMessages(int subForumID)
        {
            _logger.writeToFile("get sub forum total messages");
            if (subForumID < 0) throw new Exception("ERROR: ID is illegal");
            return _cl.subForumTotalMessages(subForumID);
        }

        public List<Post> postsByMember(int userID)
        {
            _logger.writeToFile("get posts of some member");
            if (userID < 0) throw new Exception("ERROR: ID is illegal");

            return _cl.postsByMember(userID);
        }

        public ModeratorReport moderatorReport()
        {
            _logger.writeToFile("get the moderator report");
            return _cl.moderatorReport();
        }

        //-----------Super User Reports---------------
        public int totalForums()
        {
            _logger.writeToFile("get total forum");
            return _cl.totalForums();
        }

        public List<User> membersInDifferentForums()
        {
            _logger.writeToFile("member that exist in different forum");
            return _cl.membersInDifferentForums();
        }
        //---------------------------Version 3 Use Cases End------------------------------------


        public void Clean()
        {
            _cl.Clean();
        }

    }
}
