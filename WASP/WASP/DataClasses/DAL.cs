using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public interface DAL
    {
        //forum.Id == -1
        Forum CreateForum(Forum forum);
        //forum.Id > -1
        Forum UpdateForum(Forum forum);
        //forumsId == null -> get all forums. else get all forums in the collection.
        Forum[] GetForums(int [] forumsIds);
        Forum GetForum(int id);
        //subForum.id == -1
        Subforum CreateSubForum(Subforum sf);
        //subForum.id > -1
        Subforum UpdateSubForum(Subforum sf);
        //subForumIds == null -> get all subForums. else, get all subForums in the forum.
        Subforum[] GetSubForums(int [] subForumIds);
        Subforum GetSubForum(int sfId);
        //user.id == -1
        User CreateUser(User user);
        //user.id >-1
        User updateUser(User user);
        //userIds == null -> get all users. else get all users in the forum
        User[] GetUseres(int [] userIds, Forum forum);
        User GetUser(int id, int forumId);
        //moderator.id == -1
        Moderator CreateModerator(Moderator mod);
        //moderator.id >-1
        Moderator updateModerator(Moderator mod);
        //moderatorIds == null -> get all mods. else get all mods in the forum
        Moderator[] GetModerators(int [] moderatorIds, Subforum sf);
        Moderator GetModerator(int id, int sfId);
        //Admins.id == -1
        Admin CreateAdmin(Admin admin);
        //Admins.id >-1
        Admin UpdateAdmin(Admin admin);
        //AdminIds == null -> get all admins
        Admin[] GetAdmins(int [] adminsIds, Forum forum);
        Admin GetAdmin(int adminId, int forumId);
        Post CreatePost(Post post);
        //subForum.id > -1
        Post UpdatePost(Post post);
        //subForumIds == null -> get all subForums. else, get all subForums in the forum.
        Post[] GetPosts(int [] Posts);
        Post GetPost(int postId);
        bool DeletePost(int postId);
        bool DeleteModerator(int modId, int subForumId);

        Moderator[] GetModeratorsInSubForum(int subForumID);

        Forum[] GetForumsUserID(int userId);
        bool DeleteUser(int id, int forumId);
        bool DeleteAdmin(int adminId, int forumId);
        bool DeleteForum(int forumId);
        bool DeleteSubforum(int subforumId);


        SuperUser[] GetSuperUsers(int [] superuserIds);
        SuperUser GetSuperUser(int superUserId);
        bool DeleteSuperUser(int superuserId);
        SuperUser UpdateSuperUser(SuperUser superuser);
        SuperUser CreateSuperUser(SuperUser superuser);

        Notification[] GetNotifications(int[] notificationIds);
        Notification GetNotification(int notificationId);
        bool DeleteNotification(int notificationId);
        Notification UpdateNotification(Notification notification);
        Notification CreateNotification(Notification notification);
    }
}