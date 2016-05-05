using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WASP.DataClasses
{
    public class User
    {
        public string Name { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int Id { get; set; }
        private Forum forum;
        private Dictionary<int, Post> posts = null;
        private Dictionary<int, Notification> newNotifications = null;
        private Dictionary<int, Notification> notifications = null;
        
        private DAL2 dal;



        public User(int id, string name, string username, string email, string password, Forum forum, DAL2 dal)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = null;
            this.forum = forum;
            this.dal = dal;
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
                    foreach (Post post in dal.GetUserPosts(Id))
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
            // TODO: new notification handling. 
            throw new NotImplementedException();
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
