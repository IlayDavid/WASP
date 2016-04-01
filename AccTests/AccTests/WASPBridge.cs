using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AccTests
{
    public interface WASPBridge
    {

        /*
         * Purpose: initial the system 
         * Return: a user with Admin Premissions
         */
        User initialize();

        /*
 * Porpose: returns a thread "user" to "forum", if doesnt exist returns NULL
 * Checking: if there is threadId in sf
 */
        UserThread getThread(int userId, int threadId);

        /*
         * Porpose: returns a forum with forumId, if doesnt exist returns NULL
         * Checking: suitable to forum policy
         */
        Forum getForum(int userId, int forumId);

        /*
         * Porpose: returns a subforum with forumId, if doesnt exist returns NULL
         */
        Subforum getSubforum(int userId, int subforumId);

        //----------------------------------------------------------------------------------------------------

        /*
 * Porpose: create thread
 * Checking: if there is threadId in sf
 * Return: thread id > 0
 */
        int createThread(string _userName, int _subforumId, UserThread _thread);

        /*
         * Pre-conditions: user is loged-in 
         * Purpose: create new forum which:
         *                  supervisor = user
         *                  forum = forum details
         * Return: forum id > 0        
         */
        int createForum(string userName, Forum forum);

        /* 
         * Pre-conditions: user is loged-in 
         * Purpose: create new subforum
         * Checking: if user is manager, else nothing
         * Return: subforumId > 0               
         */
        int createSubForum(string userName,int forumId, Subforum sf);

        /*
        * Porpose: to  a post at thread in  a thread "user" to "forum", if doesnt exist returns NULL
        * Checking: if there is threadId in sf
        * Return: number > 0, if success
        */
        int createPost(string userName, int threadId, Post post);


//----------------------------------------------------------------------------------------------------

        /*
* Pre-conditions: user is loged-in, user1 is a moderator of sf
* Porpose: user updates user1's term 
* Return: number > 0 if success
* Checking: if user is manager, else nothing
*/
        int updateModeratorTerm(string userName1, string userName2, int sfId, DateTime term);

        /*
         * Pre-conditions: user is loged-in 
         * Porpose: update the forum
         */
        void updateForum(int userId, int forumId);

//---------------------------------------------------------------------------------------------------

        /*
 * Porpose: set a policy for specific forum
 * Checking: if user is supervisor, else nothing
 */
        void defineForumPolicy(int userId, Forum forum);  //------------------------ policy object??

        /*
         * Porpose: subscribe "user" to "forum"
         * Return: 
         */ 
        string subscribeToForum(User user, int forumId);

        /*
         * Pre-conditions: user is loged-in 
         * Porpose: send a private message
         * Checking: if message.user exists 
         */
        void sendMessage(string userSend, string userAcc, Message message);

        /*
         * Pre-conditions: user is loged-in 
         * Porpose: user rank user1 as moderator
         * Return: number > 0 if success
         * Checking: if user is manager, else nothing
         */
        int addModerator(string userId, string userId1, int sfId, DateTime term);

        /*
         * Porpose: Confirms user's email and adds him to the forum as a member.
         */
        void confirmEmail(int userId);
        
        /*
         * Pre-conditions: user is loged-in
         * Porpose: deletes the post
         * Return: number > 0 id success
         * Checking: if user is manager, else nothing  ---------------------------------------?
         */
        int deletePost(string userName, int threadId, int postId); 
       
        /*
         * Porpose: log-in: 1 - success 0 , -1 - fail 
         * 
         */
        int login(string userName, string password);



        /*
         * Porpose: return forum's admins
         */
        List<User> getAdmins (int forumId);

        /*
         * Porpose: return forum's members
         */
        List<User> getMembers(int forumId);

        /// <summary>
        /// returns subforum of specific forum
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        List<Subforum> getSubforums(int forumId);

    }

}
