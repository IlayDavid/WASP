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
        {
            isInit = 1;
            string json = "{\"username\":\"" + userName + "\"," + "\"password\":\"" + pass + "\"," + "\"id\":" + ID
                + "," + "\"email\":\"" + email + "\"," + "\"name\":\"" + name + "\"}";
            string res = httpReq(json, "POST", _url + "/initialize/");
            return parseStringToSuperUser(res, email, name);
        }
        public int isInitialize()
        {
            return isInit;
        }

        public Forum createForum(string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            string json = "{\"password\":\"" + pass + "\"," + "\"adminid\":" + adminID
                + "," + "\"email\":\"" + email + "\"," + "\"forumname\":\"" + forumName
                + "\"," + "\"description\":\"" + description + "\"," + "\"adminusername\":\"" + adminUserName
                + "\"," + "\"adminname\":\"" + adminName + "," +
               /*policy*/  "{\"owner\":\"" + Policy.owner + "\"," + "\"moderator\":" + Policy.moderator
                + "," + "\"admin\":\"" + Policy.admin + "\"," + "\"all\":\"" + Policy.all + "}" + "\"}";
            string res = httpReq(json, "POST", _url + "/createForum/");
            return parseStringToForum(res);
        }

        public int defineForumPolicy(Policy policy)
        {
            string json = "{\"owner\":\"" + Policy.owner + "\"," + "\"moderator\":" + Policy.moderator
                + "," + "\"admin\":\"" + Policy.admin + "\"," + "\"all\":\"" + Policy.all + "}";
            string res = httpReq(json, "POST", _url + "/defineForumPolicy/");
            return parseStringToNum(res);
        } 

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            string json = "{\"userid\":" + id + "," + "\"password\":\"" + pass + "\"," + "\"username\":\"" + userName
                + "\"," + "\"email\":\"" + email + "\"," + "\"name\":\"" + name
                + "\"," + "\"forumid\":" + targetForumID + "}";
            string res = httpReq(json, "POST", _url + "/subscribeToForum/");
            return parseStringToUser(res);
        }

        public Post createThread(string title, string content, int subForumID)
        {
            string json = "{\"title\":\"" + title + "\"," + "\"content\":\"" + content
                + "\"," + "\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "/createThread/");
            return parseStringToPost(res);
        }

        public Post createReplyPost(string content, int replyToPost_ID)
        {
            string json = "{\"replytopostid\":" + replyToPost_ID + "," + "\"content\":\"" + content
                + "\"}";
            string res = httpReq(json, "POST", _url + "/createReplyPost/");
            return parseStringToPost(res);
        }

        public Subforum createSubForum(string name, string description, int moderatorID, DateTime term)
        {
            string json = "{\"name\":\"" + name + "\"," + "\"description\":\"" + description + "\"," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/createSubforum/");
            return parseStringToSubforum(res);
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}