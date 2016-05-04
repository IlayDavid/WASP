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
            string res = httpReq(json, "POST", _url + "/initialize");
            return parseStringToSuperUser(res);
        }
        public int isInitialize()
        {
            return isInit;
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            string json = "{\"userid\":" + userID + "," + "\"password\":\"" + pass + "\"," + "\"adminid\":" + adminID
                + "," + "\"email\":\"" + email + "\"," + "\"forumname\":\"" + forumName
                + "\"," + "\"description\":\"" + description + "\"," + "\"adminusername\":\"" + adminUserName
                + "\"," + "\"adminname\":\"" + adminName +/* "," +  policy here "{}" +*/ "\"}";
            string res = httpReq(json, "POST", _url + "/createForum");
            return parseStringToForum(res);
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            string json = "{\"userid\":" + id + "," + "\"password\":\"" + pass + "\"," + "\"username\":\"" + userName
                + "\"," + "\"email\":\"" + email + "\"," + "\"name\":\"" + name
                + "\"," + "\"forumid\":" + targetForumID + "}";
            string res = httpReq(json, "POST", _url + "/subscribeToForum");
            return parseStringToUser(res);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"title\":\"" + title + "\"," + "\"content\":\"" + content
                + "\"," + "\"subforumid\":" + subForumID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/createThread");
            return parseStringToPost(res);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            string json = "{\"userid\":" + userID + "," + "\"replytopostid\":" + replyToPost_ID + "," + "\"content\":\"" + content
                + "\"," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/createReplyPost");
            return parseStringToPost(res);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            string json = "{\"userid\":" + userID + "," + "\"name\":\"" + name + "\"," + "\"forumid\":" + forumID
                 + "," + "\"description\":\"" + description + "\"," + "\"moderatorid\":" + moderatorID
                 + "," + "\"termenddate\":\"" + term.ToString() + "\"}";
            string res = httpReq(json, "POST", _url + "/createSubforum");
            return parseStringToSubforum(res);
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}