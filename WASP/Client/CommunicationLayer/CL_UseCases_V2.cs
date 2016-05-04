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
        //---------------------------Version 2 Use Cases Start------------------------------------
        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            string json = "{\"userid\":" + userID + "," + "\"subforumid\":" + subForumID + "," + "\"forumid\":" + forumID
                 + "," + "\"moderatorid\":" + moderatorID
                 + "," + "{\"termenddate\":" + term.Date + "}" + "}";
            string res = httpReq(json, "POST", _url + "/addModerator");
            return parseStringToModerator(res);
        }

        public int confirmEmail(int userID, int forumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/confirmEmail");
            return 0;
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"postid\":" + postID + "}";
            string res = httpReq(json, "POST", _url + "deletePost");
            return 0;
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"message\":" + message
                + "," + "\"reciever\":" + targetUserNameID + "}";
            string res = httpReq(json, "POST", _url + "/sendMessage");
            return 0;
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            string json = "{\"userid\":" + userID + "," + "\"subforumid\":" + subforumID + "," + "\"forumid\":" + forumID
                 + "," + "\"moderatorid\":" + moderatorID
                 + "," + "{\"termenddate\":" + term.Date + "}" + "}";
            string res = httpReq(json, "POST", _url + "/updateModeratorTerm");
            return 0;
        }

        //---------------------------Version 2 Use Cases Start------------------------------------
    }
}
