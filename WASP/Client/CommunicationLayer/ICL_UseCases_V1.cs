using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    partial interface ICL
    {
        //---------------------------Version 1 Use Cases Start------------------------------------



        /*
        * Pre-conditions: none.
        * Purpose: initialize the system and logs the superuser in
        * Return: super user details.
        */
        SuperUser initialize(string name, string userName, int ID, string email, string pass);

        /*
        * Purpose: check if the system is already initialize, should be called before initialize.
        * Return: 0 - if not initialize, 1 - otherwise.
        */
        int isInitialize(string name, string userName, string email, string pass);

        /*
         * Pre-conditions: super user is loged-in 
         * Purpose: create new forum which, with details of the admin.
         * Return: forum - on succsess, NULL - in fail.
         */
        Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy);

        /*
         * Pre-conditions: superuser is loged-in 
         * Purpose: set a policy for specific forum.
         * Return: 0 - on succsess, negative - in fail.        
         */
        int defineForumPolicy(int userID, int forumID);  //------------------------ policy object??

        /*
         * Pre-conditions: none
         * Purpose: creates a Member in the forum, and return the Member.
         * Checking: forum policy on user details.
         * Return: member - on succsess, NULL - in fail. confirmEmail should be done.       
         */
        Member subscribeToForum(string userName, string name, string email, string pass, int targetForumID);

        /*
         * Pre-conditions: member is loged-in 
         * Purpose: create thread in forum.
         * Checking: forum policy on thread details.
         * Return: thread - on succsess, NULL - in fail.        
         */
        Post createThread(int userID, int forumID, string title, string content, int subForumID);

        /*
        * Pre-conditions: member is loged-in, and replypost exist.
        * Purpose: create a post to reply on an existing post (identified by postId).
        * Return: post - on succsess, NULL - in fail.
        */
        Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID);

        /* 
        * Pre-conditions: Member is loged-in and he is admin of the forum. 
        * Purpose: create new subforum in the Member's forum
        * Return: subforum - on succsess, NULL - in fail.
        */
        Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term);
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}
