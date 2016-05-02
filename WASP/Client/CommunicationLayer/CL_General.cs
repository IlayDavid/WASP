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
        enum entities { USER, SUPERUSER, POST, FORUM, SUBFORUM, MESSAGE };
        private string _url { get; set; }
        public CL()
        {

        }

        private string httpReq(string json, string method, string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        public User login(string userName, string password, int forumID)
        {
            string json = "\"user\":{\"username\":" + userName + "," + "\"password\":" + password + "," + "\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToUser(res);
        }

        private User parseStringToUser(string json)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            string json = "\"superuser\":{\"username\":" + userName + "," + "\"password\":" + password + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToSuperUser(res);
        }

        private SuperUser parseStringToSuperUser(string res)
        {
            throw new NotImplementedException();
        }

        //---------------------------------Getters----------------------------------------------

        public Post getThread(int userID, int forumID, int threadId)
        {
            string json = "\"thread\":{\"userid\":" + userID + "," + "\"threadID\":" + threadId + "," + "\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToPost(res);
        }

        private Post parseStringToPost(string res)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            string json = "\"forum\":{\"userid\":" + userID + "," + "\"forumid\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToForum(res);
        }

        public Forum getForum(int forumID)
        {
            string json = "\"forum\":{\"forumid\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToForum(res);
        }

        private Forum parseStringToForum(string res)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int userID, int forumID, int subforumId)
        {
            string json = "\"subforum\":{\"userid\":" + userID + "," + "\"forumID\":" + forumID + "," + "\"subforumID\":" + subforumId + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToSubforum(res);
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            string json = "\"subforum\":{\"forumID\":" + forumID + "," + "\"subforumID\":" + subforumId + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToSubforum(res);
        }

        private Subforum parseStringToSubforum(string res)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int userID, int forumID, int subForumID)
        {
            List<Moderator> mods = new List<Moderator>();
            Dictionary<int, Moderator> modID = getSubforum(userID, forumID, subForumID)._moderators;
            foreach (KeyValuePair<int, Moderator> entry in modID)
            {
                mods.Add(entry.Value);
            }
            return mods;
        }

        private Moderator getModerator(int userID, int forumID, int subForumID, int moderatorID)
        {
            string json = "\"moderator\":{\"userid\":" + userID + "," + "\"forumID\":" + forumID + "," + "\"subforumID\":" + subForumID + "," + "\"moderatorID\":" + moderatorID + "}";
            string res = httpReq(json, "GET", _url);
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
            string json = "\"allForum\"}";
            string res = httpReq(json, "GET", _url);
            return parseStringToForums(res);
        }

        private List<Forum> parseStringToForums(string res)
        {
            throw new NotImplementedException();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            string json = "\"allAdmins\":{\"forumID\":" + forumID + "," + "\"userID\":" + userID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToAdmins(res);
        }

        private List<Admin> parseStringToAdmins(string res)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int userID, int forumID)
        {
            throw new NotImplementedException();
            //no members in implementaion
        }

        public List<Subforum> getSubforums(int forumID)
        {
            string json = "\"allSubforums\":{\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToSubforums(res);
        }

        private List<Subforum> parseStringToSubforums(string res)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            string json = "\"admin\":{\"forumID\":" + forumID + "," + "\"userID\":" + userID +
                          "\"user\":{\"username\":" + user.name + "," + "\"userID\":" + user.id + "," + "\"password\":" + user.password + "}" +
                            "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToAdmin(res);
        }

        private Admin parseStringToAdmin(string res)
        {
            throw new NotImplementedException();
        }
    }
}
