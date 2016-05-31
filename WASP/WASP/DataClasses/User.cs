using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WASP.DataClasses
{
    public class User : Authority
    {
        public override Authority.Level AuthorizationLevel()
        {
            return Authority.Level.User;
        }
        public string Name { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PasswordChangeDate { get; set; }





        public int Id { get; set; }
        private Forum forum;
        private Dictionary<int, Post> posts = null;
        private Dictionary<int, Notification> newNotifications = null;
        private Dictionary<int, Notification> notifications = null;
        private Dictionary<int, User> friends = null;
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static User Get(int userId, int forumId)
        {
            return dal.GetUser(userId, forumId);
        }
        public static User[] Get(int[] ids, int forumID)
        {
            return dal.GetUsers(ids, forumID);
        }

        public User Update()
        {
            User old = Get(Id, Forum.Id);
            if (!old.Password.Equals(Password))
            {
                PasswordChangeDate = DateTime.Now;
            }
            return dal.UpdateUser(this);
        }

        public User Create()
        {
            return dal.CreateUser(this);
        }

        public bool Delete()
        {
            return dal.DeleteUser(this.Id, this.Forum.Id);
        }

        public User(int id, string name, string username, string email, string password, Forum forum)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = null;
            this.forum = forum;
            this.PasswordChangeDate = DateTime.Now;
            this.StartDate = DateTime.Now;
        }

        public User(int id, string name, string username, string email, string password, Forum forum, DateTime startDate, DateTime passChangeDate)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = null;
            this.forum = forum;
            this.PasswordChangeDate = passChangeDate;
            this.StartDate = startDate;
        }

        // DEPRECATED
        public User(int id, string name, string username, string email, string password, Forum forum, DAL2 dal)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = null;
            this.forum = forum;
            this.PasswordChangeDate = DateTime.Now;
            this.StartDate = DateTime.Now;
        }

        // DEPRECATED
        public User(int id, string name, string username, string email, string password, Forum forum, DateTime startDate, DateTime passChangeDate, DAL2 dal)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = null;
            this.forum = forum;
            this.PasswordChangeDate = passChangeDate;
            this.StartDate = startDate;
        }


        private Dictionary<int, User> Friends
        {
            get
            {
                if (friends == null)
                {
                    friends = new Dictionary<int, User>();
                    foreach (User friend in dal.GetUserFriends(Id, Forum.Id))
                    {
                        friends.Add(friend.Id, friend);
                    }
                }
                return Friends;
            }
        }
        private Dictionary<int, Notification> Notifications
        {
            get
            {
                if (notifications == null)
                {
                    notifications = new Dictionary<int, Notification>();
                    foreach (Notification notif in dal.GetUserNotifications(Id))
                    {
                        notifications.Add(notif.Id, notif);
                    }
                }
                return notifications;
            }
        }
        private Dictionary<int, Notification> NewNotifications
        {
            get
            {
                if (newNotifications == null)
                {
                    newNotifications = new Dictionary<int, Notification>();
                    foreach (Notification notif in dal.GetUserNewNotifications(Id))
                    {
                        newNotifications.Add(notif.Id, notif);
                    }
                }
                return newNotifications;
            }
        }
        private Dictionary<int, Post> Posts
        {
            get
            {
                if (this.posts == null)
                {
                    posts = new Dictionary<int, Post>();
                    foreach (Post post in dal.GetUserPosts(Id, forum.Id))
                    {
                        posts.Add(post.Id, post);
                    }
                }
                return posts;
            }
        }
        public Post[] GetAllPosts()
        {
            Post[] postArr = new Post[Posts.Values.Count];
            Posts.Values.CopyTo(postArr, 0);
            return postArr;
        }
        public User[] GetAllFriends()
        {
            User[] frnds = new User[Friends.Values.Count];
            Friends.Values.CopyTo(frnds, 0);
            return frnds;
        }
        public void AddFriend(User friend)
        {
            this.Friends.Add(friend.Id, friend);
        }
        public Forum Forum
        {
            get
            {
                return forum;
            }
            set
            {
                forum = value;
            }
        }




        public Post GetPost(int post_ID)
        {
            Post thePost;
            Posts.TryGetValue(post_ID, out thePost);
            return thePost;
        }
        public void RemovePost(int post_ID)
        {
            Posts.Remove(post_ID);
        }
        public void AddPost(Post post)
        {
            Posts.Add(post.Id, post);
        }

        public void NewNotification(Notification newNotification)
        {
            newNotification.Target = this;
            newNotification = newNotification.Create();
            NewNotifications.Add(newNotification.Id, newNotification);
            Config.Settings.NotificationMethod(Id, forum.Id);
        }

        public Notification[] GetAllNotifications()
        {
            Notification[] notifs = new Notification[Notifications.Values.Count];
            Notifications.Values.CopyTo(notifs, 0);
            return notifs;
        }

        public Notification[] GetNewNotifications()
        {

            Notification[] notifs = new Notification[NewNotifications.Values.Count];
            NewNotifications.Values.CopyTo(notifs, 0);
            return notifs;
        }
    }
}
