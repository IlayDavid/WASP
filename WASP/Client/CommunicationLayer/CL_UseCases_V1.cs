using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Client.CommunicationLayer;

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
                + "\"," + "\"adminname\":\"" + adminName + "\"}"; /*"," +
               policy  "{\"owner\":\"" + Policy.owner + "\"," + "\"moderator\":" + Policy.moderator
                + "," + "\"admin\":\"" + Policy.admin + "\"," + "\"all\":\"" + Policy.all + "}" + "\"}";*/
            string res = httpReq(json, "POST", _url + "/createForum/");
            return parser.parseStringToForum(res);
        }

        public int defineForumPolicy(Policy policy)
        {
            string json = "{\"owner\":\"" + Policy.owner + "\"," + "\"moderator\":" + Policy.moderator
                + "," + "\"admin\":\"" + Policy.admin + "\"," + "\"all\":\"" + Policy.all + "}";
            string res = httpReq(json, "POST", _url + "/defineForumPolicy/");
            return parser.parseStringToNum(res);
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
                + "\"," + "\"subforumid\":" + subForumID  +",\"auth\":\"" + _auth + "\"}";
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