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
        * Return: number >= 0 id success        
        */
        int deleteModerator(int userID, int forumID, int moderatorID, int subForumID);

        //-----------Admin Reports---------------
        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return the total number of messages posted in the entire forum. */
        int subForumTotalMessages(int userID, int forumID, int subForumID);

        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return the total number of messages written by member. */
        int memberTotalMessages(int userID, int forumID);

        /* Pre-conditions: Member is loged-in, and is admin of the forum
         * Purpose: return details about moderators in all subforums,
         *          for each moderator who appoint him, when, 
         *          which subforum he belongs to, and what the messages they posted. */
        ModeratorReport moderatorReport(int userID, int forumID);

        //-----------Super User Reports---------------
        /* Pre-conditions: Member is loged-in, and is superuser.
         * Purpose: return number of the forums in the system.*/
        int totalForums(int userID);


        /* Pre-conditions: Member is loged-in, and is superuser.
         * Purpose: return members that subscribe to more than one forum.*/
        List<Member> membersInDifferentForums(int userID);

        //---------------------------Version 3 Use Cases End------------------------------------
    }
}
