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
            string json = "{\"postid\":" + postID + "," + "\"auth\":\"" + _auth + "\"," + "\"content\":\"" + content + "\"}";
            string res = httpReq(json, "POST", _url + "/editPost/");
            return 0;
        }

        public int deleteModerator(int moderatorID, int subForumID)
        {
            string json = "{\"subforumid\":" + subForumID + "," + "\"auth\":\"" + _auth + "\"," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/deleteModerator/");
            return 0;
        }

        public Admin addAdmin(int newAdminID)
        {   //username, id, password, email, name
            string json = "{\"newadminid\":" + newAdminID + "," + "\"auth\":\"" + _auth + "\"" + "}";
            string res = httpReq(json, "POST", _url + "/addAdmin/");
            return parser.parseStringToAdmin(res, this);
        }

        public List<Notification> getAllNotificationses()
        {
            string json = "{\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/getAllNotificationses/");
            return parser.parseStringToMessages(res, false);
        }
        public List<Notification> getNewNotificationses()
        {
            string json = "{\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/getNewNotificationses/");
            return parser.parseStringToMessages(res, true);
        }

        //-----------Admin Reports---------------
        public int subForumTotalMessages(int subForumID)
        {
            string json = "{\"subforumid\":" + subForumID + "," + "\"auth\":\"" + _auth + "\"," + "}";
            string res = httpReq(json, "POST", _url + "/subForumTotalMessages/");
            return parser.parseStringToNum(res);
        }



        public List<Post> postsByMember(int userID)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"userid\":" + userID + "," + "\"auth\":\"" + _auth + "\"," + "}";
            string res = httpReq(json, "POST", _url + "/postsByMember/");
            return parser.parseStringToPosts(res);
        }

        public ModeratorReport moderatorReport()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/moderatorReport/");
            return parser.parseStringToModeratorReport(res);
        }


        //-----------Super User Reports---------------
        public int totalForums()
        {
            string json = "{\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/totalForums/");
            return parser.parseStringToNum(res);
        }

        public List<User> membersInDifferentForums()
        {   //username, id, password, email, name
            string json = "{\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/membersInDifferentForums/");
            return parser.parseStringToUsers(res);
        }

        //---------------------------Version 3 Use Cases End------------------------------------

        //---------------------------Version 4 Use Cases-----------------------------------

        public List<User> getFriends()
        {
            string json = "{\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/getFriends/");
            return parser.parseStringToFriends(res);
        }

        /* Pre-conditions: User is loged-in.
        * Purpose: add user with friendID to the loged-in user's friend list.*/
        public int addFriend(int friendID)
        {
            string json = "{\"friend\":" + friendID + "," + "\"auth\":\"" + _auth + "\"," + "}";
            string res = httpReq(json, "POST", _url + "/addFriend/");
            return 0;
        }
    }
}
