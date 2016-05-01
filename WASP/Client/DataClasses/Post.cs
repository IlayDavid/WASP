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
            _id = counter++;
            _title = title;
            _content = content;
            _author = author;
            _publishedAt = DateTime.Now;
            _editAt = _publishedAt;
            _containerID = container;

            _replies = new List<Post>();

            _inReplyTo = inReplyTo;
        }

        public string _title{ get; set; }
        public string _content{ get; set; }
        public User _author{ get; set; }
        public DateTime _publishedAt { get; set; }
        public DateTime _editAt { get; set; }
        public int _id{ get; set; }
        public int _containerID{ get; set; }
        public Post _inReplyTo{ get; set; }
        
        //should be null at first request
        public List<Post> _replies { get; set; }
}
}