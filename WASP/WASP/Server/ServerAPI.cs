﻿using System;
using System.Collections.Generic;
using WASP.DataClasses;

namespace WASP
{
    public interface ServerAPI
    {
        /*
        * Purpose: initialize the system and logs the superuser in
        * Return: a Member with Admin Premissions
        */
        SuperUser initialize(String name, String userName, String email, String pass);

        /*
         * Porpose: returns a thread "Member" to "Forum", if doesnt exist returns NULL
         * Checking: if there is threadId in sf
         * post condition: result is an opening post
         */
        Post getThread(Member member, int threadId);

        /*
         * Porpose: returns a Forum with forumId, if doesnt exist returns NULL
         * Checking: suitable to Forum policy
         */
        Forum getForum(Member member, int forumId);

        /*
         * Porpose: returns a subforum with forumId, if doesnt exist returns NULL
         */
        Subforum getSubforum(Member member, int subforumId);

        //----------------------------------------------------------------------------------------------------

        /*
        * Porpose: create thread
        * Checking: if there is threadId in sf
        * Return: the created thread
        */
        Post createThread(Member author,String title, String content, DateTime now, Subforum container);

        /*
         * Pre-conditions: Member is loged-in 
         * Purpose: create new Forum which:
         *                  supervisor = Member
         *                  Forum = Forum details
         * Return: created Forum        
         */

        Forum createForum(SuperUser creator, String forumName, String description, String userName, String adminName, String email, String pass);

        /// <summary>
        /// </summary>
        /// <param name="subforumId"></param>
        /// <returns> subforum's moderatores </returns>
        List<Member> getModerators(Member member, Subforum subforum);

        /// <summary>
        /// </summary>
        /// <param name="member"> the Member requesting the information</param>
        /// <param name="moderator"> the moderator we request the information about</param>
        /// <param name="subforumId"> which of subforum we talk about</param>
        /// <returns> date of moderator's term time</returns>
        DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum);

        /* 
         * Pre-conditions: Member is loged-in 
         * Purpose: create new subforum in the Member's Forum
         * Checking: if Member is manager, else nothing
         * Return: subforumId > 0               
         */
        Subforum createSubForum(Member member, String name, String description);
        /// <summary>
        /// </summary>
        /// <returns> all system's forums</returns>
        List<Forum> getAllForums(User member);

        /*
        * Porpose: to create a post to reply on an existing post (identified by postId)
        * Checking: if there is threadId in sf
        * Return: number > 0, if success
        */
        Post createReplyPost(Member Author, String title, String content, DateTime now, Post inReplyTo, Subforum container);


        //----------------------------------------------------------------------------------------------------

        /*
* Pre-conditions: Member is loged-in, user1 is a moderator of sf
* Porpose: Member updates moderator's term (new term=term)
* Return: number > 0 if success
* Checking: if Member is manager, else nothing
*/
        int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term);

        //---------------------------------------------------------------------------------------------------

        /*
 * Porpose: set a policy for specific Forum
 * Checking: if Member is supervisor, else nothing
 */
        int defineForumPolicy(SuperUser member, Forum forum);  //------------------------ policy object??

        /*
         * Porpose: creates a Member in the Forum, logs him in, and return the Member
         * Return: 
         */
        Member subscribeToForum(String userName, String name,  String email, String pass, Forum targetForum);

        /*
         * Pre-conditions: Member is loged-in 
         * Porpose: send a private message
         * Checking: if message.Member exists 
         */
        int sendMessage(Member member, Member targetMember, Message message);

        /*
         * Pre-conditions: Member is loged-in 
         * Porpose: Member rank user1 as moderator
         * Return: number >= 0 if success
         * Checking: if Member is manager, else nothing
         */
        int addModerator(Member member, Member moderator, Subforum subforum, DateTime term);

        /*
         * Porpose: Confirms Member's email and adds him to the Forum as a member.
         * return: number>=0 if success
         */
        int confirmEmail(Member member);

        /*
         * Pre-conditions: Member is loged-in
         * Porpose: deletes the post
         * Return: number >= 0 id success
         * Checking: if Member is manager, else nothing
         */
        int deletePost(Member member, Post post);

        /*
         * Porpose: log-in: return the Member, logged in, to the specified Forum.
         */
        Member login(string userName, string password, Forum forum);



        /*
         * Porpose: return Forum's admins
         */
        List<Member> getAdmins(User member, Forum forum);

        /*
         * Porpose: return Forum's members
         */
        List<Member> getMembers(Member member, Forum forum);

        /// <summary>
        /// returns subforum of specific Forum
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        List<Subforum> getSubforums(Member member, Forum forum);


    }
}