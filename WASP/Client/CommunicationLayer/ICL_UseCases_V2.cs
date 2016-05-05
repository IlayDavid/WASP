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
        //---------------------------Version 2 Use Cases Start------------------------------------
        /*
         * Pre-conditions: Member is loged-in, second member is exists. 
         * Purpose: send a private message.
         */
        int sendMessage(string targetUserNameID, string message);

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

    }
}
