using System;
using System.Collections.Generic;

namespace Client.DataClasses
{
    public class Post
    {
        public string _title{ get; set; }
        public string _content{ get; set; }
        public Member _author{ get; set; }
        public DateTime _publishedAt { get; set; }
        public DateTime _editAt { get; set; }
        public int _id{ get; set; }
        public int _containerID{ get; set; }
        public Post _inReplyTo{ get; set; }
        public List<int> _repliesIDs { get; set; }

        //should be null at first request
        public List<Post> _replies { get; set; }
}
}