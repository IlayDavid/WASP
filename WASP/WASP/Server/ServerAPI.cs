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
        
        string login(string password, string username);

        /// <summary>
        /// Create a forum.
        /// Pre condition: Initiating user must be a superuser and logged in, Forum has no ID.
        /// Gets a new forum and checks if valid. If valid returns some form of text, if not, returns error. Forum will have an ID after creation.
        /// </summary>
        /// <returns></returns>
        string createForum(User user, Forum forum);
        
        /// <summary>
        /// Update a forum
        /// Pre condition: Initiating user must be a superuser/admin of the forum and logged in. Forum already has an ID in the system.
        /// Updates the forum in the system to match the new changes.
        /// </summary>
        /// <returns></returns>
        string updateForum(User user, Forum forum);

        /// <summary>
        /// Subscribe to forum
        /// Pre condition: User is logged in and not a member of the forum.
        /// Will send confirmation email to the user if all of the constraints demanded by the forum are met.
        /// </summary>
        /// <returns></returns>
        string subscribeToForum(User user, Forum forum);

        /// <summary>
        /// Confirming email
        /// Pre condition: User is logged in to the system. Current instance of confirmation email HAS NOT been expended/expired.
        /// Confirms user's email and adds him to the forum as a member.
        /// </summary>
        /// <returns></returns>
        string confirmEmail(User user);

        /// <summary>
        /// Creates a new subforum according to specification
        /// Precondition: User must be logged in and an admin of the forum.
        /// Subforum must meet the constraints set by the forum (number of mods, etc).
        /// </summary>
        /// <returns></returns>
        string createSubForum(User user, Subforum sf);

        /// <summary>
        /// Adds a moderator to the subforum
        /// Precondition: User must be logged in and an admin of the forum the subforum resides in.
        /// Adds a moderator to the subforum with a date of expiration for his term. Should the user already be a moderator of the subforum, his term will be updated.
        /// </summary>
        /// <returns></returns>
        string addModerator(User user, User moderator, Subforum sf, DateTime term);

        /// <summary>
        /// Creates a new post in a subforum.
        /// Precondition: User must be logged in and a member of the forum.
        /// </summary>
        /// <returns></returns>
        string createPost(User user, Subforum sf, Thread thread, Post post);

        /// <summary>
        /// Send a private message to user.
        /// Precondition: User must be logged in to the system. Message.user must be registered to the system.
        /// Sends a private message to target.
        /// </summary>
        /// <returns></returns>
        string sendMessage(User user, Message message);
   }

    public class Forum
    {
    }
}
