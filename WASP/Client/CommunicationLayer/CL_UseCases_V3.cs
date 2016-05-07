using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Client.CommunicationLayer
{
    public partial class CL : ICL
    {
        //---------------------------Version 3 Use Cases Start------------------------------------
        public int editPost(int postID, string content)
        {
            string json = "{\"postid\":" + postID
                + "," + "\"content\":\"" + content + "\"}";
            string res = httpReq(json, "POST", _url + "/editPost/");
            return 0;
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            string json = "{\"subforumid\":" + subForumID + "," +  "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/deleteModerator/");
            return 0;
        }

        public Admin addAdmin(int newAdminID)
        {
            string json = "{\"newadminid\":" + newAdminID + "}";
            string res = httpReq(json, "POST", _url + "/addAdmin/");
            return parser.parseStringToAdmin(res, this);
        }

        public List<Message> getAllNotificationses()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/getAllNotificationses/");
            return parseStringToMessages(res);
        }
        public List<Message> getNewNotificationses()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/getNewNotificationses/");
            return parseStringToMessages(res);
        }

        private List<Message> parseStringToMessages(string res)
        {
            throw new NotImplementedException();
        }

        //-----------Admin Reports---------------
        public int subForumTotalMessages(int subForumID)
        {
            string json = "{\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "/subForumTotalMessages/");
            return parseStringToNum(res);
        }

        private int parseStringToNum(string res)
        {
            throw new NotImplementedException();
        }

        public List<Post> postsByMember(int userID)
        {
            string json = "{\"userid\":" + userID +"}";
            string res = httpReq(json, "POST", _url + "/postsByMember/");
            return parser.parseStringToPosts(res);
        }

        public ModeratorReport moderatorReport()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/moderatorReport/");
            return parseStringToModeratorReport(res);
        }

        private ModeratorReport parseStringToModeratorReport(string res)
        {
            throw new NotImplementedException();
        }

        //-----------Super User Reports---------------
        public int totalForums()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/totalForums/");
            return parseStringToNum(res);
        }

        public List<User> membersInDifferentForums()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/membersInDifferentForums/");
            return parser.parseStringToUsers(res);
        }

        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
