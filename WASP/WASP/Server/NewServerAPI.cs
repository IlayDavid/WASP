using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;
using WASP.DataClasses.Policies;

namespace WASP.Server
{
    public interface NewServerAPI
    {
        /*
       * Purpose: initialize the system and logs the superuser in
       * Return: a Member with Admin Premissions
       */
        SuperUser initialize(string name, string userName, string email, string pass);

        /*
         * Porpose: returns a thread "Member" to "forum", if doesnt exist returns NULL
         * Checking: if there is threadId in sf
         * post condition: result is an opening post
         */
        Post getThread(int userID, int forumID, int threadId);

        /*
         * Porpose: returns a forum with forumId for userID, if doesnt exist returns NULL
         * Checking: suitable to forum policy
         */
        Forum getForum(int userID, int forumID);


        /*
        * Porpose: returns a forum with forumId for GUEST, if doesnt exist returns NULL
        * Checking: suitable to forum policy
        */
        Forum getForum(int forumID);

        /*
         * Porpose: returns a subforum with forumId for userID, if doesnt exist returns NULL
         */
        Subforum getSubforum(int userID, int forumID, int subforumId);

        /*
         * Porpose: returns a subforum with forumId for GUEST, if doesnt exist returns NULL
         */
        Subforum getSubforum(int forumID, int subforumId);

        //----------------------------------------------------------------------------------------------------

        /*
        * Porpose: create thread
        * Checking: if there is threadId in sf
        * Return: the created thread
        */
        Post createThread(int userID, int forumID, string title, string content, DateTime now, int subForumID);

        /*
         * Pre-conditions: Member is loged-in 
         * Purpose: create new forum which:
         *                  supervisor = Member
         *                  forum = forum details
         * Return: created forum        
         */

        Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy);

        /// <summary>
        /// </summary>
        /// <param name="subforumId"></param>
        /// <returns> subforum's moderatores </returns>
        List<Member> getModerators(int userID, int forumID, int subForumID);

        /// <summary>
        /// </summary>
        /// <param name="member"> the Member requesting the information</param>
        /// <param name="moderator"> the moderator we request the information about</param>
        /// <param name="subforumId"> which of subforum we talk about</param>
        /// <returns> date of moderator's term time</returns>
        DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID);

        /* 
         * Pre-conditions: Member is loged-in 
         * Purpose: create new subforum in the Member's forum
         * Checking: if Member is manager, else nothing
         * Return: subforumId > 0               
         */
        Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term);

        
        // <returns> all system's forums</returns>
        List<Forum> getAllForums();

        /*
        * Porpose: to create a post to reply on an existing post (identified by postId)
        * Checking: if there is threadId in sf
        * Return: number > 0, if success
        */
        Post createReplyPost(int userID, int forumID, string content, DateTime now, int replyToPost_ID);

        //----------------------------------------------------------------------------------------------------

        /*
        * Pre-conditions: Member is loged-in, user1 is a moderator of sf
        * Porpose: Member updates moderator's term (new term=term)
        * Return: number > 0 if success
        * Checking: if Member is manager, else nothing
        */
        int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term);

        //---------------------------------------------------------------------------------------------------

        /*
 * Porpose: set a policy for specific forum
 * Checking: if Member is supervisor, else nothing
 */
        int defineForumPolicy(int userID, int forumID);  //------------------------ policy object??

        /*
         * Porpose: creates a Member in the forum, logs him in, and return the Member
         * Return: 
         */
        Member subscribeToForum(string userName, string name, string email, string pass, int targetForumID);

        /*
         * Pre-conditions: Member is loged-in 
         * Porpose: send a private message
         * Checking: if message.Member exists 
         */
        int sendMessage(int userID, int forumID, string targetUserNameID, string message);

        /*
         * Pre-conditions: Member is loged-in 
         * Porpose: Member rank user1 as moderator
         * Return: number >= 0 if success
         * Checking: if Member is manager, else nothing
         */
        int addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term);

        /*
         * Porpose: Confirms Member's email and adds him to the forum as a member.
         * return: number>=0 if success
         */
        int confirmEmail(int userID, int forumID);

        /*
         * Pre-conditions: Member is loged-in
         * Porpose: deletes the post
         * Return: number >= 0 id success
         * Checking: if Member is manager, else nothing
         */
        int deletePost(int userID, int forumID, int postID);

        /*
         * Porpose: log-in: return the Member, logged in, to the specified forum.
         */
        Member login(string userName, string password, int forumID);

        /*
         * Porpose: log-in only for super user. 
         */
        SuperUser loginSU(string userName, string password);
        /*
         * Porpose: return forum's admins
         */
        List<Member> getAdmins(int userID, int forumID);

        /*
         * Porpose: return forum's members
         */
        List<Member> getMembers(int userID, int forumID);

        /// <summary>
        /// returns subforum of specific forum
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        List<Subforum> getSubforums(int userID, int forumID);

        Member getAdmin(User user, int forumID, int userID);
    }
}
