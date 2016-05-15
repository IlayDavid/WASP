using System;
using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Post
    {
        public static int counter = 0;

        public Post() { }
        public Post(string title, string content, User author, int container, Post inReplyTo)
        {
            id = counter++;
            this.title = title;
            this.content = content;
            this.author = author;
            publishedAt = DateTime.Now;
            editAt = publishedAt;
            containerID = container;

            replies = new List<Post>();

            this.inReplyTo = inReplyTo;
        }

        public string title{ get; set; }
        public string content{ get; set; }
        public User author{ get; set; }
        public DateTime publishedAt { get; set; }
        public DateTime editAt { get; set; }
        public int id{ get; set; }
        public int containerID{ get; set; }
        public Post inReplyTo{ get; set; }
        
        //should be null at first request
        public List<Post> replies { get; set; }
}
}