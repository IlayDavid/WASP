using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface IBL
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
        int isInitialize();

        /*
         * Pre-conditions: super user is loged-in 
         * Purpose: create new forum which, with details of the admin.
         * Return: forum - on succsess, NULL - in fail.
         */
        Forum createForum(string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy);

        /*
         * Pre-conditions: superuser (or Admin) is loged-in 
         * Purpose: set a policy for specific forum.
         * Return: 0 - on succsess, negative - in fail.        
         */
        int defineForumPolicy(Policy policy);
        /*
         * Pre-conditions: none
         * Purpose: creates a Member in the forum, and return the Member.
         * Checking: forum policy on user details.
         * Return: member - on succsess, NULL - in fail. confirmEmail should be done.       
         */
        User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID);

        /*
         * Pre-conditions: user is loged-in 
         * Purpose: create thread in forum.
         * Checking: forum policy on thread details.
         * Return: thread - on succsess, NULL - in fail.        
         */
        Post createThread(string title, string content, int subForumID);

        /*
        * Pre-conditions: user is loged-in, and replypost exist.
        * Purpose: create a post to reply on an existing post (identified by postId).
        * Return: post - on succsess, NULL - in fail.
        */
        Post createReplyPost(string content, int replyToPost_ID);

        /* 
        * Pre-conditions: Admin is loged-in and he is admin of the forum. 
        * Purpose: create new subforum in the Member's forum
        * Return: subforum - on succsess, NULL - in fail.
        */
        Subforum createSubForum(string name, string description, int moderatorID, DateTime term);
        //---------------------------Version 1 Use Cases End------------------------------------

        //---------------------------Version 2 Use Cases Start------------------------------------
        /*
         * Pre-conditions: Member is loged-in, second member is exists. 
         * Purpose: send a private message.
         */
        int sendMessage(int targetUserID, string message);

        /*
         * Pre-conditions: Admin (or Moderator) is loged-in and is admin of the forum, moderator is member of the forum.
         * Purpose: appoint moderator to the subforum.
         * Return: number >= 0 if success.
         */
        Moderator addModerator(int moderatorID, int subForumID, DateTime term);

        /*
        * Pre-conditions: Admin is loged-in, and is admin of the forum, moderator exist.
        * Purpose: Member updates moderator's term (new term=term)
        * Return: number > 0 if success
        */
        int updateModeratorTerm(int subforumID, int moderatorID, DateTime term);

        /*
        * Purpose: Confirms Member's email and adds him to the forum as an active member.
        * return: number>=0 if success
        */
        int confirmEmail(int code);

        /*
        * Pre-conditions: User (or Admin, Moderator) is loged-in, and own the post. (or manager, depend on policy)
        * Purpose: deletes the post
        * Return: number >= 0 id success
        */
        int deletePost(int postID);

        //---------------------------Version 2 Use Cases End------------------------------------




        //---------------------------Version 3 Use Cases Start------------------------------------

        /* 
        * Pre-conditions: User is loged-in, and own the post.  
        * Purpose: edit post (by id) written by member with id userID 
        * Return: number >= 0 id success        
        */
        int editPost(int postID, string content);

        /*  
        * Pre-conditions: Admin is loged-in, and appoint the moderator.
        * Purpose: delete moderator from subforum, 
        * Return: number >= 0 if success        
        */
        int deleteModerator(int moderatorID, int subForumID);

        //1: interactivity. forum should push new notifications to the users. regardless, the user should be able to get the notifications.
        List<Notification> getAllNotificationses();
        List<Notification> getNewNotificationses();
        /*  
        * Pre-conditions: super user (or Admin) is loged-in.
        * Purpose: add admin to forum.
        * Return: number >= 0 if success        
        */
        Admin addAdmin(int newAdminID);
        //-----------Admin Reports---------------
        /* Pre-conditions: Admin is loged-in, and is admin of the forum
         * Purpose: return the total number of messages posted in the entire forum. */
        int subForumTotalMessages(int subForumID);

        /* Pre-conditions: Admin is loged-in, and is admin of the forum
         * Purpose: return the total messages written by member. */
        List<Post> postsByMember(int userID);

        /* Pre-conditions: Admin is loged-in, and is admin of the forum
         * Purpose: return details about moderators in all subforums,
         *          for each moderator who appoint him, when, 
         *          which subforum he belongs to, and what the messages they posted. */
        ModeratorReport moderatorReport();

        //-----------Super User Reports---------------
        /* Pre-conditions: Super User is loged-in, and is superuser.
         * Purpose: return number of the forums in the system.*/
        int totalForums();

        /* Pre-conditions: Super User is loged-in, and is superuser.
         * Purpose: return members that subscribe to more than one forum.*/
        List<User> membersInDifferentForums();

        //---------------------------Version 3 Use Cases End------------------------------------

        
            
        //---------------------------Version 4 Use Cases Start------------------------------------

        //login by client-session password (requested in ass3)
        User loginBySession(string session);

        //---------------------------Version 4 Use Cases End------------------------------------


        //only for comunication layer.
        void setForumID(int forumID);

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
        Post getThread(int threadId);

        /*
        * Purpose: returns 'amount' threads of subforums. start with thread 'from'. 
        */
        List<Post> getThreads(int subForumID);

        /*
        * Purpose: returns replays of some tread in subforums. start with thread 'from'. 
        */
        List<Post> getReplys(int postID);

        /* Purpose: returns a forum with forumId, if doesnt exist returns NULL */
        Forum getForum(int forumID);

        /* Purpose: returns a subforum with forumId, if doesnt exist returns NULL */
        Subforum getSubforum(int subforumId);

        /* Purpose: returns modrators of subforum. */
        List<Moderator> getModerators(int subForumID);

        /*
        * Pre-conditions: 
        * Purpose: return the date of moderator's term time. */
        DateTime getModeratorTermTime(int moderatorID, int subforumID);

        /* Purpose: return information about all the existing forums. */
        List<Forum> getAllForums();

        /* Purpose: return forum's admins information. */
        List<Admin> getAdmins(int forumID);

        /* Purpose: return forum's members information. */
        List<User> getMembers(int forumID);

        /* Purpose: return forum's subForums information. */
        List<Subforum> getSubforums(int forumID);

        /* Purpose: return forum's Admin information. */
        Admin getAdmin(int AdminID, int forumID); 
    }
}
