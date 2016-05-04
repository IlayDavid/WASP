using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial interface ICL
    {/* 
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
        List<Post> getThreads(int forumID, int subForumID, int from, int amount);

        /*
        * Purpose: returns replays of some tread in subforums. start with thread 'from'. 
        */
        List<Post> getReplies(int forumID, int subForumID, int postID);

        /* Purpose: returns a forum with forumId, if doesnt exist returns NULL */
        Forum getForum(int forumID);

        /* Purpose: returns a subforum with forumId, if doesnt exist returns NULL */
        Subforum getSubforum(int forumID, int subforumId);

        /* Purpose: returns modrators of subforum. */
        List<Moderator> getModerators(int forumID, int subForumID);

        /* Purpose: return the date of moderator's term time. */
        DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID);

        /* Purpose: return information about all the existing forums. */
        List<Forum> getAllForums();

        /* Purpose: return forum's admins information. */
        List<Admin> getAdmins(int userID, int forumID);

        /* Purpose: return forum's members information. */
        List<User> getMembers(int userID, int forumID);

        /* Purpose: return forum's subForums information. */
        List<Subforum> getSubforums(int forumID);

        /* Purpose: return forum's Admin information. */
        Admin getAdmin(int userID, int forumID, int AdminID);

    }
}
