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
            string json = "\"User\":{\"userName\":" + userName + "," + "\"password\":" + password + "," + "\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToUser(res);
        }

        private User parseStringToUser(string json)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            string json = "\"SuperUser\":{\"userName\":" + userName + "," + "\"password\":" + password + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToSuperUser(res);
        }

        private SuperUser parseStringToSuperUser(string res)
        {
            throw new NotImplementedException();
        }

        //---------------------------------Getters----------------------------------------------

        public Post getThread(int forumID, int threadId)
        {
            string json =  "\"Post\":{\"threadId\":" + threadId + "," + "\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToPost(res);
        }

        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            throw new NotImplementedException();
        }
        public List<Post> getReplys(int forumID, int subForumID, int postID)
        {
            throw new NotImplementedException();
        }
        private Post parseStringToPost(string res)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            string json = "\"Forum\":{\"userID\":" + userID + "," + "\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToForum(res);
        }

        public Forum getForum(int forumID)
        {
            string json = "\"Forum\":{\"forumID\":" + forumID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToForum(res);
        }

        private Forum parseStringToForum(string res)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            string json = "\"Subforum\":{\"forumID\":" + forumID + "," + "\"subforumId\":" + subforumId + "}";
            string res = httpReq(json, "GET", _url);
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
            string json = "\"Moderator\":{\"userID\":" + userID + "," + "\"forumID\":" + forumID + "," + "\"subForumID\":" + subForumID + "," + "\"moderatorID\":" + moderatorID + "}";
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
            string json = "\"allForums\"}";
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

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            string json = "\"Admin\":{\"forumID\":" + forumID + "," + "\"userID\":" + userID + "," + "\"AdminID\":" + AdminID + "}";
            string res = httpReq(json, "GET", _url);
            return parseStringToAdmin(res);
        }

        private Admin parseStringToAdmin(string res)
        {
            throw new NotImplementedException();
        }
    }
}
