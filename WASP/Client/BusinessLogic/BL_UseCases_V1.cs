﻿using Client.CommunicationLayer;
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

        public Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            string validStr = isUserValid(adminName, adminUserName, 1, email, pass);
            if (validStr == null) {
                if (!IsStrValid(forumName))
                    validStr = "ERROR: Forum name is empty";
                if (!IsStrValid(description))
                    validStr = "ERROR: Forum description is empty";
            }

            if (validStr == null)
                return _cl.createForum(userID, forumName, description, adminUserName, adminName, email, pass, policy);
            else
                throw new Exception(validStr);
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(string userName, string name, string email, string pass, int targetForumID)
        {
            throw new NotImplementedException();
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            throw new NotImplementedException();
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            throw new NotImplementedException();
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}
