using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial class CL : ICL
    {
        //---------------------------Version 3 Use Cases Start------------------------------------
        public int editPost(int userID, int forumID, int postID, string content)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"postid\":" + postID
                + "," + "\"content\":\"" + content + "\"}";
            string res = httpReq(json, "POST", _url + "/editPost");
            return 0;
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"subforumid\":" + subForumID + "," + "\"forumid\":" + forumID
                 + "," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/deleteModerator");
            return 0;
        }
        public List<Message> getAllNotificationses(int userID, int forumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getAllNotificationses");
            return parseStringToMessages(res);
        }
        public List<Message> getNewNotificationses(int userID, int forumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getNewNotificationses");
            return parseStringToMessages(res);
        }

        private List<Message> parseStringToMessages(string res)
        {
            throw new NotImplementedException();
        }

        //-----------Admin Reports---------------
        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "/subForumTotalMessages");
            return parseStringToNum(res);
        }

        private int parseStringToNum(string res)
        {
            throw new NotImplementedException();
        }

        public List<Post> postsByMember(int adminID, int forumID, int userID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"adminid\":" + adminID + "}";
            string res = httpReq(json, "POST", _url + "postsByMember");
            return parseStringToPosts(res);
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        //-----------Super User Reports---------------
        public int totalForums(int userID)
        {
            string json = "{\"userid\":" + userID + "}";
            string res = httpReq(json, "POST", _url + "/totalForums");
            return parseStringToNum(res);
        }

        public List<User> membersInDifferentForums(int userID)
        {
            string json = "{\"userid\":" + userID + "}";
            string res = httpReq(json, "POST", _url + "/membersInDifferentForums");
            return parseStringToUsers(res);
        }

        private List<User> parseStringToUsers(string res)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
