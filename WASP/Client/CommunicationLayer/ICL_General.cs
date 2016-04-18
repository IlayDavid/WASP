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
        /* 
       * Pre-conditions: member is subscribe to the forum.
       * Purpose: logged in, to the specified forum.
       * Return: return the Member, if success, NULL otherwise.
       */
        Member login(string userName, string password, int forumID);

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
        Post getThread(int userID, int forumID, int threadId);

        /* Purpose: returns a forum with forumId for userID, if doesnt exist returns NULL */
        Forum getForum(int userID, int forumID);

        /* Purpose: returns a forum with forumId for GUEST, if doesnt exist returns NULL */
        Forum getForum(int forumID);

        /* Purpose: returns a subforum with forumId for userID, if doesnt exist returns NULL */
        Subforum getSubforum(int userID, int forumID, int subforumId);

        /* Purpose: returns a subforum with forumId for GUEST, if doesnt exist returns NULL */
        Subforum getSubforum(int forumID, int subforumId);

        /* Purpose: returns modrators of subforum. */
        List<Member> getModerators(int userID, int forumID, int subForumID);

        /* Purpose: return the date of moderator's term time. */
        DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID);

        /* Purpose: return information about all the existing forums. */
        List<Forum> getAllForums();

        /* Purpose: return forum's admins information. */
        List<Member> getAdmins(int userID, int forumID);

        /* Purpose: return forum's members information. */
        List<Member> getMembers(int userID, int forumID);

        /* Purpose: return forum's subForums information. */
        List<Subforum> getSubforums(int userID, int forumID);

        /* Purpose: return forum's Admin information. */
        Member getAdmin(User user, int forumID, int userID);
    }
}
