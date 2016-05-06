using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WASP.DataClasses
{
    public interface DAL2
    {
        void Clean();
        void Backup();
        void Restore();
        //forum.Id == -1

        Forum CreateForum(Forum forum);
        //forum.Id > -1
        Forum UpdateForum(Forum forum);
        //forumsId == null -> get all forums. else get all forums in the collection.
        Forum[] GetForums(int[] forumsIds);
        Forum GetForum(int id);
        User[] GetForumMembers(int forumID);
        Admin[] GetForumAdmins(int forumID);
        Policy GetForumPolicy(int forumID);
        Subforum[] GetForumSubForums(int forumID);





        //subForum.id == -1
        Subforum CreateSubForum(Subforum sf);
        //subForum.id > -1
        Subforum UpdateSubForum(Subforum sf);
        //subForumIds == null -> get all subForums. else, get all subForums in the forum.
        Subforum[] GetSubForums(int[] subForumIds);
        Subforum GetSubForum(int sfId);
        Moderator[] GetSubForumMods(int subForumID);
        Post[] GetSubForumPosts(int subForumID);
        Forum GetSubForumForum(int subForumID);

        //user.id == -1
        User CreateUser(User user);
        //user.id >-1
        User UpdateUser(User user);
        //userIds == null -> get all users. else get all users in the forum
        User[] GetUsers(int[] userIds, int forumId);
        User GetUser(int id, int forumId);
       
        
        Post[] GetUserPosts(int userID,int forumID);
        Notification[] GetUserNewNotifications(int userID);
        Notification[] GetUserNotifications(int userID);

        //moderator.id == -1
        Moderator CreateModerator(Moderator mod);
        //moderator.id >-1
        Moderator UpdateModerator(Moderator mod);

        /*
         * In getmoderators and getmoderator the mod.user must be defined.
         */
        //moderatorIds == null -> get all mods. else get all mods in the forum
        Moderator[] GetModerators(int[] moderatorIds, Subforum sf);
        Moderator GetModerator(int id, int sfId);

        Subforum GetModeratorSubForum(int modID);
        Admin GetModeratorAppointerAdmin(int modID);


        //Admins.id == -1
        Admin CreateAdmin(Admin admin);
        //Admins.id >-1
        Admin UpdateAdmin(Admin admin);

        /*
         * In getadmins and getadmin the admin.user must be defined.
         */
        //AdminIds == null -> get all admins
        Admin[] GetAdmins(int[] adminsIds, Forum forum);
        Admin GetAdmin(int adminId, int forumId);

        Moderator[] GetAdminAppointedMods(int adminID);
        Forum GetAdminForum(int adminID);

        Post CreatePost(Post post);
        //subForum.id > -1
        Post UpdatePost(Post post);
        //subForumIds == null -> get all subForums. else, get all subForums in the forum.
        Post[] GetPosts(int[] Posts);
        Post GetPost(int postId);
        Subforum GetPostSubforum(int postID);
        //get the post PostID replied to
        Post GetInReplyTo(int postID);
        //get all replies to postID
        Post[] GetReplies(int PostID);
        User GetPostAuthor(int postID);


        //return all the forums in which userID is a member
        Forum[] GetForumsUserID(int userId);


        // Deleted
        bool DeleteModerator(int modId, int subForumId);
        bool DeleteUser(int id, int forumId);
        bool DeleteAdmin(int adminId, int forumId);
        bool DeleteForum(int forumId);
        bool DeleteSubforum(int subforumId);
        bool DeletePost(int postID);











        SuperUser[] GetSuperUsers(int[] superuserIds);
        SuperUser GetSuperUser(int superUserId);
        bool DeleteSuperUser(int superuserId);
        SuperUser UpdateSuperUser(SuperUser superuser);
        SuperUser CreateSuperUser(SuperUser superuser);

        Notification[] GetNotifications(int[] notificationIds);
        Notification GetNotification(int notificationId);
        bool DeleteNotification(int notificationId);
        Notification UpdateNotification(Notification notification);
        Notification CreateNotification(Notification notification);
        User GetNotificationSource(int id);
        User GetNotificationTarget(int id);


        Admin[] GetAdminsOfForum(Forum forum);
        Post[] GetPostsInSubForum(int id);
        Post[] GetPostsOfUser(int userId, int forumId);
        Post[] GetReplysPost(int id);
        Moderator[] GetAppointedModsOfAdmin(int adminId, int forumid);
    }
}
