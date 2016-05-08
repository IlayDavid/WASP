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
    }
}
