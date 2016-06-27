using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial class CL : BusinessLogic.IBL
    {
        //---------------------------Version 2 Use Cases Start------------------------------------
        public Moderator addModerator(int moderatorID, int subForumID, DateTime term)
        {   //moderatorid, appointedbyid, subforumid
            string json = "{\"subforumid\":" + subForumID + "," + "\"auth\":\"" + _auth + "\"," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/addModerator/");
            return parser.parseStringToModerator(res, term);
        }

        public int confirmEmail(int code)
        {
            string json = "{\"code\":" + code + "," + "\"auth\":\"" + _auth + "\"," + "}";
            string res = httpReq(json, "POST", _url + "/confirmEmail/");
            return 0;
        }

        public int deletePost(int postID)
        {
            string json = "{\"postid\":" + postID + "," + "\"auth\":\"" + _auth + "\"" + "}";
            string res = httpReq(json, "POST", _url + "/deletePost/");
            return 0;
        }

        public int sendMessage(int targetUserID, string message)
        {
            string json = "{\"message\":\"" + message + "\"," + "\"auth\":\"" + _auth + "\"," + "\"target\":" + targetUserID + "}";
            string res = httpReq(json, "POST", _url + "/sendMessage/");
            return 0;
        }

        public int updateModeratorTerm(int moderatorID, int subforumID, DateTime term)
        {
            string json = "{\"subforumid\":" + subforumID + "," + "\"auth\":\"" + _auth + "\"," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/updateModeratorTerm/");
            return 0;
        }

        public DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            string json = "{\"subforumid\":" + subforumID + "," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/getModeratorTermTime/");
            return parser.parseStringToDate(res);
        }

        //---------------------------Version 2 Use Cases Start------------------------------------
    }
}
