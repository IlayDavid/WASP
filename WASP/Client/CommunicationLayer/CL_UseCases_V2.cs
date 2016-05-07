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
        public Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {
            string json = "{\"subforumid\":" + subForumID + "," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/addModerator/");
            return parser.parseStringToModerator(res);
        }

        public int confirmEmail(int code)
        {
            string json = "{\"code\":" + code  + "}";
            string res = httpReq(json, "POST", _url + "/confirmEmail/");
            return 0;
        }

        public int deletePost(int postID)
        {
            string json = "{\"postid\":" + postID + "}";
            string res = httpReq(json, "POST", _url + "/deletePost/");
            return 0;
        }

        public int sendMessage(string targetUserNameID, string message)
        {
            string json = "{\"message\":\"" + message
                + "\"," + "\"reciever\":\"" + targetUserNameID + "\"}";
            string res = httpReq(json, "POST", _url + "/sendMessage/");
            return 0;
        }

        public int updateModeratorTerm(int moderatorID, int subforumID, DateTime term)
        {
            string json = "{\"subforumid\":" + subforumID + "," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/updateModeratorTerm/");
            return 0;
        }

        DateTime ICL.getModeratorTermTime(int moderatorID, int subforumID)
        {
            string json = "{\"subforumid\":" + subforumID + "," + "\"moderatorid\":" + moderatorID
                 + "\"}";
            string res = httpReq(json, "POST", _url + "/getModeratorTermTime/");
            return parseStringToDate(res);
        }

        //---------------------------Version 2 Use Cases Start------------------------------------
    }
}
