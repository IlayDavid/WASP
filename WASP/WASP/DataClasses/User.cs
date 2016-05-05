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
        public Forum forum { get; set; }
        public Dictionary<int, Post> posts;
        public Queue<Notification> newNotifications=new Queue<Notification>();
        public List<Notification> notifications=new List<Notification>();
        

        public User(int id, string name, string username,string email,string password,Forum forum)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.posts = new Dictionary<int, Post>();
            this.forum = forum;
            
        }


        public Post[] GetAllPosts()
        {
            Post[] postArr = new Post[posts.Values.Count];
            posts.Values.CopyTo(postArr, 0);
            return postArr;
        }

        public Post GetPost(int post_ID)
        {
            Post thePost;
            posts.TryGetValue(post_ID, out thePost);
            return thePost;
        }
        public void RemovePost(int post_ID)
        {
            posts.Remove(post_ID);
        }
        public void AddPost(Post post)
        {
            posts.Add(post.Id, post);
        }
        
        public void NewNotification(Notification newNotification)
        {
            // TODO: new notification handling. 
            throw new NotImplementedException();
        }

        public Notification[] GetAllNotifications()
        {
            return this.notifications.ToArray();
        }

        public Notification[] GetNewNotifications()
        {
            return this.newNotifications.ToArray();
        }
    }
}
