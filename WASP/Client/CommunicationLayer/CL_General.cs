using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
//using System.Net;
//using System.Text;  // for class Encoding

namespace Client.CommunicationLayer
{

    //add check to method in httpReq
    //add rel url to field
    //implement pasresing strings to objects
    //getModerator return value
    //no members in implementaion
    public partial class CL : ICL
    {
        private string _url { get; set; }
        public CL()
        {

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
        {
            string json = "{\"username\":" + userName + "," + "\"password\":" + password + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/login");
            return parseStringToUser(res);
        }

        private User parseStringToUser(string json)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            string json = "{\"username\":" + userName + "," + "\"password\":" + password + "}";
            string res = httpReq(json, "POST", _url + "/loginSU");
            return parseStringToSuperUser(res);
        }

        private SuperUser parseStringToSuperUser(string res)
        {
            throw new NotImplementedException();
        }

        //---------------------------------Getters----------------------------------------------

        public Post getThread(int forumID, int threadId)
        {
            string json = "{\"postid\":" + threadId + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getThread");
            return parseStringToPost(res);
        }

        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            throw new NotImplementedException();
        }
        public List<Post> getReplies(int forumID, int subForumID, int postID)
        {
            string json = "{\"postid\":" + postID + "," + "\"forumid\":" + forumID + "," + "\"subforumid\":" + subForumID + "}";
            string res = httpReq(json, "POST", _url + "getReplies");
            return parseStringToPosts(res);
        }

        private List<Post> parseStringToPosts(string res)
        {
            throw new NotImplementedException();
        }

        private Post parseStringToPost(string res)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getForum");
            return parseStringToForum(res);
        }

        public Forum getForum(int forumID)
        {
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getForum");
            return parseStringToForum(res);
        }

        private Forum parseStringToForum(string res)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            string json = "{\"forumid\":" + forumID + "," + "\"subforumid\":" + subforumId + "}";
            string res = httpReq(json, "POST", _url + "/getSubforum");
            return parseStringToSubforum(res);
        }

        private Subforum parseStringToSubforum(string res)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            List<Moderator> mods = new List<Moderator>();
            Dictionary<int, Moderator> modID = getSubforum(forumID, subForumID).moderators;
            foreach (KeyValuePair<int, Moderator> entry in modID)
            {
                mods.Add(entry.Value);
            }
            return mods;
        }

        private Moderator getModerator(int userID, int forumID, int subForumID, int moderatorID)
        {
            string json = "{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "," + "\"subforumid\":" + subForumID + "," + "\"moderatorid\":" + moderatorID + "}";
            string res = httpReq(json, "POST", _url + "/getModerator");
            return parseStringToModerator(res);
        }

        private Moderator parseStringToModerator(string res)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            Moderator mod = getModerator(userID, forumID, subforumID, moderatorID);
            //return mod.termTime 
            //moderator in client dont have properties
            return new DateTime();
        }

        public List<Forum> getAllForums()
        {
            string json = "";
            string res = httpReq(json, "POST", _url + "/getAllForums");
            return parseStringToForums(res);
        }

        private List<Forum> parseStringToForums(string res)
        {
            throw new NotImplementedException();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            string json = "{\"forumid\":" + forumID + "," + "\"userid\":" + userID + "}";
            string res = httpReq(json, "POST", _url + "/getAdmins");
            return parseStringToAdmins(res);
        }

        private List<Admin> parseStringToAdmins(string res)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int userID, int forumID)
        {
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getMembers");
            return parseStringToUsers(res);
        }

        public List<Subforum> getSubforums(int forumID)
        {
            string json = "{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "POST", _url + "/getSubforums");
            return parseStringToSubforums(res);
        }

        private List<Subforum> parseStringToSubforums(string res)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            string json = "{\"forumid\":" + forumID + "," + "\"userid\":" + userID + "," + "\"adminid\":" + AdminID + "}";
            string res = httpReq(json, "POST", _url + "/getAdmin");
            return parseStringToAdmin(res);
        }

        private Admin parseStringToAdmin(string res)
        {
            throw new NotImplementedException();
        }

        public Admin addAdmin(int adminID, int forumID, int newAdminID)
        {
            string json = "{\"forumid\":" + forumID + "," + "\"newadminid\":" + newAdminID + "," + "\"adminid\":" + adminID + "}";
            string res = httpReq(json, "POST", _url + "/addAdmin");
            return parseStringToAdmin(res);
        }
    }
}
