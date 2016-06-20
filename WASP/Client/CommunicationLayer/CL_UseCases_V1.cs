using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Client.CommunicationLayer;
using System.Web.Script.Serialization;

namespace Client.CommunicationLayer
{
    public partial class CL : ICL
    {
        int isInit = 0;//0 not init, 1 init


        //---------------------------Version 1 Use Cases Start------------------------------------
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {   //username, id, password, auth
            isInit = 1;
            string json = "{\"username\":\"" + userName + "\"," + "\"password\":\"" + pass + "\"," + "\"id\":" + ID
                + "," + "\"email\":\"" + email + "\"," + "\"name\":\"" + name + "\"}";
            string res = httpReq(json, "POST", _url + "/initialize/");
            return parser.parseStringToSuperUser(res, email, name, this);
        }
        public int isInitialize()
        {
            string json = "{}";
            string res = httpReq(json, "POST", _url + "/isInitialize/");
            return int.Parse(res); //1 init, 0 not init
        }

        public Forum createForum(string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {   // title, description, moderatorid, 
            string json = "{\"password\":\"" + pass + "\"," + "\"adminid\":" + adminID + "," + "\"auth\":\"" + _auth + "\","
                + "\"email\":\"" + email + "\"," + "\"forumname\":\"" + forumName
                + "\"," + "\"description\":\"" + description + "\"," + "\"adminusername\":\"" + adminUserName
                + "\"," + "\"adminname\":\"" + adminName + "\"}";
            string res = httpReq(json, "POST", _url + "/createForum/");
            return parser.parseStringToCreateForum(res);
        }

        public int defineForumPolicy(Policy policy)
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            dict.Add("auth", _auth);
            dict.Add("forum", forumID);
            string delete = getDeleteString(policy.deletePost);
            dict.Add("deletepost", delete);
            dict.Add("emailverf", policy.emailVerification);
            dict.Add("questions", policy.questions);
            dict.Add("usersload", policy.usersSameTime);
            TimeSpan password = new TimeSpan(policy.passwordPeriod, 0, 0, 0, 0);
            TimeSpan seniority = new TimeSpan(policy.seniority, 0, 0, 0, 0);
            dict.Add("passperiod", password);
            dict.Add("seniority", seniority);
            string json = jss.Serialize(dict);
            string res = httpReq(json, "POST", _url + "/defineForumPolicy/");
            return parser.parseStringToNum(res);
        }

        private string getDeleteString(int deletePost)
        {
            string ret = "";
            switch (deletePost)
            {
                case 1:
                    ret = "Owner";
                    break;
                case 2:
                    ret = "Moderator";
                    break;
                case 3:
                    ret = "OwnerAndModerator";
                    break;
                case 4:
                    ret = "Admin";
                    break;
                case 5:
                    ret = "OwnerAndAdmin";
                    break;
                case 6:
                    ret = "ModeratorAndAdmin";
                    break;
                case 7:
                    ret = "OwnerModeratorAndAdmin";
                    break;
            }
            return ret;
        }


        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {   //username, id, password, name, email
            string json = "{\"userid\":" + id + "," + "\"password\":\"" + pass + "\"," + "\"username\":\"" + userName
                + "\"," + "\"email\":\"" + email + "\"," + "\"name\":\"" + name
                + "\"," + "\"forumid\":" + targetForumID + "}";
            string res = httpReq(json, "POST", _url + "/subscribeToForum/");
            return parser.parseStringToUser(res, false, this);
        }

        public Post createThread(string title, string content, int subForumID)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"title\":\"" + title + "\"," + "\"content\":\"" + content
                + "\"," + "\"subforumid\":" + subForumID + ",\"auth\":\"" + _auth + "\"}";
            string res = httpReq(json, "POST", _url + "/createThread/");
            return parser.parseStringToPost(res);
        }

        public Post createReplyPost(string content, int replyToPost_ID)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"replytopostid\":" + replyToPost_ID + "," + "\"auth\":\"" + _auth + "\"," + "\"content\":\"" + content
                + "\"}";
            string res = httpReq(json, "POST", _url + "/createReplyPost/");
            return parser.parseStringToPost(res, true);
        }

        public Subforum createSubForum(string name, string description, int moderatorID, DateTime term)
        {   //name, description, moderatorid
            string json = "{\"name\":\"" + name + "\"," + "\"description\":\"" + description + "\"," + "\"auth\":\"" + _auth + "\"," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/createSubForum/");
            return parser.parseStringToSubforum(res);
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}