//using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;
using WASP.DataClasses.Reports;

namespace WASP.Domain
{
    public interface IBL
    {
        void Clean();
        void Restore();
        void Backup();
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
        int isInitialize();

        /*
         * Pre-conditions: super user is loged-in 
         * Purpose: create new forum which, with details of the admin.
         * Return: forum - on succsess, NULL - in fail.
         */
        Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy);

        /*
         * Pre-conditions: superuser is loged-in 
         * Purpose: set a policy for specific forum.
         * Return: 0 - on succsess, negative - in fail.        
         */
        int defineForumPolicy(int userID, int forumID, string deletePost, TimeSpan passwordPeriod, bool emailVerification, TimeSpan minimumSeniority, int usersLoad, string[] questions, bool notifyOffline, bool superUser = false); //------------------------ policy object??

        /*
         * Pre-conditions: none
         * Purpose: creates a Member in the forum, and return the Member.
         * Checking: forum policy on user details.
         * Return: member - on succsess, NULL - in fail. confirmEmail should be done.       
         */
        User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID, string[] answers, bool wantNotifications=true);

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

        //---------------------------Version 2 Use Cases Start------------------------------------
        /*
         * Pre-conditions: Member is loged-in, second member is exists. 
         * Purpose: send a private message.
         */
        int sendMessage(int userID, int forumID, int targetUserNameID, string message);

        /*
         * Pre-conditions: Member is loged-in and is admin of the forum, moderator is member of the forum.
         * Purpose: appoint moderator to the subforum.
         * Return: number >= 0 if success.
         */
        Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term);

        /*
        * Pre-conditions: Member is loged-in, and is admin of the forum, moderator exist.
        * Purpose: Member updates moderator's term (new term=term)
        * Return: number > 0 if success
        */
        int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term);

        /*
        * Purpose: Confirms Member's email and adds him to the forum as an active member.
        * return: number>=0 if success
        */
        int confirmEmail(int userID, int forumID);

        /*
        * Pre-conditions: Member is loged-in, and own the post. (or manager, depend on policy)
        * Purpose: deletes the post
        * Return: number >= 0 id success
        */
        int deletePost(int userID, int forumID, int postID);

        //---------------------------Version 2 Use Cases End------------------------------------




        //---------------------------Version 3 Use Cases Start------------------------------------

        /* 
        * Pre-conditions: Member is loged-in, and own the post.  
        * Purpose: edit post (by id) written by member with id userID 
        * Return: number >= 0 id success        
        */
        int editPost(int userID, int forumID, int postID, string content);

        /*  
        * Pre-conditions: Member is loged-in, and is admin of the forum.
        * Purpose: delete moderator from subforum, 
        * Return: number >= 0 if success        
        */
        int deleteModerator(int userID, int forumID, int moderatorID, int subForumID);
        Admin addAdmin(int userID, int forumID, int adminId);
        //1: interactivity. forum should push new notifications to the users. regardless, the user should be able to get the notifications.
        Notification[] getAllNotificationses(int userID, int forumID);
        Notification[] getNewNotificationses(int userID, int forumID);
        //-----------Admin Reports---------------
        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return the total number of messages posted in the entire forum. */
        int subForumTotalMessages(int userID, int forumID, int subForumID, bool superUser=false);

        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return the total messages written by member. */
        Post[] postsByMember(int adminID, int forumID, int userID, bool superUser = false);

        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return details about moderators in all subforums,
         *          for each moderator who appoint him, when, 
         *          which subforum he belongs to, and what the messages they posted. */
        ModeratorReport moderatorReport(int userID, int forumID, bool superUser = false);

        //-----------Super User Reports---------------
        /* Pre-conditions: Member is loged-in, and is superuser.
         * Purpose: return number of the forums in the system.*/
        int totalForums(int userID);

        /* Pre-conditions: Member is loged-in, and is superuser.
         * Purpose: return members that subscribe to more than one forum.*/
        User[] membersInDifferentForums(int userID);
        //---------------------------Version 3 Use Cases End------------------------------------




        /* 
     * Pre-conditions: member is subscribe to the forum.
     * Purpose: logged in, to the specified forum.
     * Return: return the Member, if success, NULL otherwise.
     */
        User login(string userName, string password, int forumID);

        /*
        * Pre-conditions: member is super user.
        * Purpose: logged in as super user, to the the Forums system.
        * Return: return the Member, if success, NULL otherwise.
        */
        SuperUser loginSU(string userName, string password);

        //---------------------------------Getters----------------------------------------------

        /*
         * Purpose: returns a thread by id, if doesnt exist returns NULL
         * post condition: result is an opening post
         */
        Post getThread(int forumID, int threadId);

        /*
        * Purpose: returns 'amount' threads of subforums. start with thread 'from'. 
        */
        Post[] getThreads(int subForumID);

        /*
        * Purpose: returns replays of some tread in subforums. start with thread 'from'. 
        */
        Post[] getReplys(int forumID, int subForumID, int postID);

        /* Purpose: returns a forum with forumId for userID, if doesnt exist returns NULL */
        Forum getForum(int forumID);


        /* Purpose: returns a subforum with forumId for userID, if doesnt exist returns NULL */
        Subforum getSubforum(int forumID, int subforumId);

        /* Purpose: returns modrators of subforum. */
        Moderator[] getModerators(int forumID, int subForumID);

        /* Purpose: return the date of moderator's term time. */
        DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID);

        /* Purpose: return information about all the existing forums. */
        Forum[] getAllForums();

        /* Purpose: return forum's admins information. */
        Admin[] getAdmins(int userID, int forumID);

        /* Purpose: return forum's members information. */
        User[] getMembers(int userID, int forumID);

        /* Purpose: return forum's subForums information. */
        Subforum[] getSubforums(int forumID);

        /* Purpose: return forum's Admin information. */
        Admin getAdmin(int userID, int forumID, int AdminID);

        User[] getFriends(int userID, int forumID);

        int addFriend(int userID, int forumID, int friendID);
        int restorePasswordByAnswers(int userID, int forumID, string[] answers, string newPassword);
    }
}