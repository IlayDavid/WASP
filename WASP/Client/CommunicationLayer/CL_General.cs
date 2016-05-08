using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
//using System.Net;
//using System.Text;  // for class Encoding

namespace Client.CommunicationLayer
{

    //policy and dateTime
    //admin
    public partial class CL : ICL
    {
        private string _url { get; set; }
        public string _auth { get; set; }
        private ParseString parser;

        public CL()
        {
            _url = "http://localhost:8080";
            parser = new ParseString(); 
        }

        private string httpReq(string json, string method, string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            httpWebRequest.ContentLength = json.Length;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if ((int)httpResponse.StatusCode != 200)
            {
                return HandleHttpError((int)httpResponse.StatusCode);
            }
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        private string HandleHttpError(int statusCode)
        {
            throw new NotImplementedException();
        }


        public User login(string userName, string password, int forumID)
        {   //username, id, auth, password, email, name
            string json = "{\"username\":\"" + userName + "\"," + "\"password\":\"" + password + "\"," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/login/");
            return parser.parseStringToUser(res, true, this);
        }

        public SuperUser loginSU(string userName, string password)
        {   //username, id, auth, password, email, name
            string json = "{\"username\":\"" + userName + "\"," + "\"password\":\"" + password + "\"}";
            string res = httpReq(json, "POST", _url + "/loginSU/");
            return parser.parseStringToSuperUser(res, this);
        }

        //---------------------------------Getters----------------------------------------------

        public Post getThread(int threadId)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"postid\":" + threadId + "}";
            string res = httpReq(json, "POST", _url + "/getThread/");
            return parser.parseStringToPost(res);
        }

        public List<Post> getThreads(int subForumID)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "/getThreads/");
            return parser.parseStringToPosts(res);
        }
        public List<Post> getReplys(int postID)
        {   //title,  content,  authorid,  subforumid,  replypostid
            string json = "{\"postid\":" + postID + "}";
            string res = httpReq(json, "POST", _url + "/getReplys/");
            return parser.parseStringToPosts(res);
        }     

        public Forum getForum(int forumID)
        {   //name, description, adminid
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getForum/");
            return parser.parseStringToForum(res);
        }       

        public Subforum getSubforum(int subforumId)
        {   //name, description, moderatorid
            string json = "{\"subforumid\":" + subforumId + "}";
            string res = httpReq(json, "POST", _url + "/getSubforum/");
            return parser.parseStringToSubforum(res);
        }

        DateTime getModeratorTermTime(int moderatorID, int subforumID)
        {
            string json = "{\"subforumid\":" + subforumID + "," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/getModeratorTermTime/");
            return parseStringToDate(res);
        }

        private DateTime parseStringToDate(string res)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int subForumID)
        {   //subforumid, appointedbyid, moderatorid
            string json = "{\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "/getModerators/");
            return parser.parseStringToModerators(res);
        }

        private Moderator getModerator(int userID, int forumID, int subForumID, int moderatorID)
        {   //moderatorid, appointedbyid, subforumid
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"subforumid\":" + subForumID + "," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/getModerator/");
            return parser.parseStringToModerator(res);
        }

        public List<Forum> getAllForums()
        {   //name, description, adminid
            string json = "{\"auth\":" + _auth + "}";
            string res = httpReq(json, "POST", _url + "/getAllForums/");
            return parser.parseStringToForums(res);
        }

        public List<Admin> getAdmins(int forumID)
        {   //username, id, password, email, name
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getAdmins/");
            return parser.parseStringToAdmins(res);
        }

        public List<User> getMembers(int forumID)
        {   //username, id, password, email, name
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getMembers/");
            return parser.parseStringToUsers(res);
        }

        public List<Subforum> getSubforums(int forumID)
        {   //subforumid, name, description
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getSubforums/");
            return parser.parseStringToSubforums(res);
        }

        public Admin getAdmin(int AdminID)
        {   //username, id, password, email, name
            string json = "{\"adminid\":" + AdminID + "}";
            string res = httpReq(json, "POST", _url + "/getAdmin/");
            return parser.parseStringToAdmin(res, this);
        }

    }
}
