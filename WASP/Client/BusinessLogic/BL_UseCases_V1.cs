using Client.CommunicationLayer;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public partial class BL : IBL
    {
        
        //---------------------------Version 1 Use Cases Start------------------------------------
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            string validStr = isUserValid(name, userName, ID, email, pass);
            if (validStr == null)
                return _cl.initialize(name, userName, ID, email, pass);
            else
                throw new Exception(validStr);
        }

        private string isUserValid(string name, string userName, int iD, string email, string pass)
        {
            if (!IsStrValid(name)) return "ERROR: name is illegal!";
            if (!IsStrValid(userName)) return "ERROR: userName is illegal!";
            if (!IsEmailValid(email)) return "ERROR: email format is illegal!";
            if (!IsPasswordValid(pass)) return "ERROR: password is illegal!";
            if (iD < 0) return "ERROR: id is illegal!";

            return null;
        }

        private bool IsEmailValid(string email)
        {
            return true;
        }

        private bool IsPasswordValid(string email)
        {
            return true;
        }

        private bool IsStrValid(string str){ return (str != null && !str.Equals("")); }

        public int isInitialize()
        {
            return _cl.isInitialize();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            string validStr = isUserValid(adminName, adminUserName, adminID, email, pass);
            if (validStr != null) throw new Exception(validStr);
            if (userID < 0) throw new Exception("ERROR: ID is illegal");
            if (!IsStrValid(forumName)) throw new Exception("ERROR: Forum name is empty");
            if (!IsStrValid(description)) throw new Exception("ERROR: Forum description is empty");

            return _cl.createForum(userID, forumName, description, adminID, adminUserName, adminName, email, pass, policy);

        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            string errorMsg = isUserValid(name, userName, id, email, pass);
            if (targetForumID < 0) throw new Exception("ERROR: ID is illegal");
            if (errorMsg != null) throw new Exception(errorMsg);

            return _cl.subscribeToForum(id, userName, name, email, pass, targetForumID);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            if (userID < 0 || forumID < 0 || subForumID < 0) throw new Exception("ERROR: ID is illegal");
            if (!IsStrValid(title)) throw new Exception("ERROR: Post title is empty");
            if (!IsStrValid(content)) throw new Exception("ERROR: Post content is empty");

            return _cl.createThread(userID, forumID, title, content, subForumID);
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            if (userID < 0 || forumID < 0 || replyToPost_ID < 0) throw new Exception("ERROR: ID is illegal");
            if (!IsStrValid(content)) throw new Exception("ERROR: Post content is empty");

            return _cl.createReplyPost(userID, forumID, content, replyToPost_ID);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            if (userID < 0 || forumID < 0 || moderatorID < 0) throw new Exception("ERROR: ID is illegal");
            if (!IsStrValid(name)) throw new Exception("ERROR: name is empty or illegal");
            if (!IsStrValid(description)) throw new Exception("ERROR: description is empty or illegal");
            if (term.Date.CompareTo(DateTime.Now.Date) <= 0)throw new Exception("ERROR: Date should be after: "+ DateTime.Now.Date.ToShortDateString());

            return _cl.createSubForum(userID, forumID, name, description, moderatorID, term);
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}
