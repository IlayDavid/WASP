using System;

namespace WASP
{
    public interface ServerAPI
    {
        /// <summary>
        /// Initializes the system.
        /// At the end there will be a super user account with the following credentials:
        /// Username: admin
        /// Password: Wasp1234Sting
        /// </summary>
        /// <returns></returns>
        string initialize();
        
        /// <summary>
        /// Logging to the system.
        /// Returns an error if Username and Passwords do not match.
        /// Returns authentication hashcode and public user information (Name, email, usergroup, etc.)
        /// </summary>
        /// <returns></returns>
        
        string login(string username, string password);

        /// <summary>
        /// Create a forum.
        /// Pre condition: Initiating user must be a superuser and logged in, Forum has no ID.
        /// Gets a new forum and checks if valid. If valid returns some form of text, if not, returns error. Forum will have an ID after creation.
        /// </summary>
        /// <returns></returns>
        string createForum(int user_ID, Forum forum);
        
        /// <summary>
        /// Update a forum
        /// Pre condition: Initiating user must be a superuser/admin of the forum and logged in. Forum already has an ID in the system.
        /// Updates the forum in the system to match the new changes.
        /// </summary>
        /// <returns></returns>
        string updateForum(int user_ID, Forum forum);

        /// <summary>
        /// Subscribe to forum
        /// Pre condition: User is logged in and not a member of the forum.
        /// Will send confirmation email to the user if all of the constraints demanded by the forum are met.
        /// </summary>
        /// <returns></returns>
        string subscribeToForum(User user, int forum_ID);

        /// <summary>
        /// Confirming email
        /// Pre condition: User is logged in to the system. Current instance of confirmation email HAS NOT been expended/expired.
        /// Confirms user's email and adds him to the forum as a member.
        /// </summary>
        /// <returns></returns>
        string confirmEmail(int user_ID);

        /// <summary>
        /// Creates a new subforum according to specification
        /// Precondition: User must be logged in and an admin of the forum.
        /// Subforum must meet the constraints set by the forum (number of mods, etc).
        /// </summary>
        /// <returns></returns>
        string createSubForum(int user_ID, Subforum sf);

        /// <summary>
        /// Adds a moderator to the subforum
        /// Precondition: User must be logged in and an admin of the forum the subforum resides in.
        /// Adds a moderator to the subforum with a date of expiration for his term. Should the user already be a moderator of the subforum, his term will be updated.
        /// </summary>
        /// <returns></returns>
        string addModerator(int user_ID, int moderator_ID, int sf_ID, DateTime term);

        /// <summary>
        /// Creates a new post in a subforum.
        /// Precondition: User must be logged in and a member of the forum.
        /// </summary>
        /// <returns></returns>
        string createPost(int user_ID, int thread_ID, Post post);

        /// <summary>
        /// Send a private message to user.
        /// Precondition: User must be logged in to the system. Message.user must be registered to the system.
        /// Sends a private message to target.
        /// </summary>
        /// <returns></returns>
        string sendMessage(int user_ID, Message message);

        // Get existing forum
        // return forum with id-forumId.
        // the forum's mode (Supervisor, manager, Member) is set according to the user permission.
        // for a guest: user=NULL.
        string getForum(int user_ID, int forum_ID);

        // Get existing sub-forum
        // return sub-forum in forum with id-forumId.
        // the sub-forum's mode (Supervisor, manager, Member) is set
        // according to the sub-forum policy and user permission.
        // for a guest: user=NULL.
        string getSubForum(int user_ID, int sf_ID);

        // Get existing thread in sub-forum
        // return thread in sub-forum in forum with id-forumId.
        // the thread's mode (Supervisor, manager, Member) is set
        // according to the sub-forum policy and user permission.
        // for a guest: user=NULL.
        string getThread(int user_ID, int thread_ID);

        // Define policy for a forum
        // Pre condition: Initiating user must be a superuser of the forum and logged in. 
        // Forum is in process of creation.
        // Define the forum's policy in the system.
        string defineForumPolicy(int user_ID, Forum forum);

        // Create thread - new discussion in sub-forum
        // Precondition: User must be logged in to the system.
        //               User must be registered to the forum.
        string createThread(int user_ID, int sf_ID, Thread thread);

        // Delete a post in a sub-forum.
        // Precondition: User must be logged in and a member of the forum.
        string deletePost(int user_ID, int thread_ID, int post);

        // Update term of moderator
        // Precondition: User must be logged in and an admin of the forum the subforum resides in.
        // Update term of moderator in sub-forum.
        string updateModeratorTerm(int user_ID, int moderator_ID, int sf_ID, DateTime term);
    }
}