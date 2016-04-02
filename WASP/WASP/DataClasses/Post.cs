using System;
using System.Collections.Generic;

namespace WASP
{
    public class Post
    {
        private String title, content;
        private User author;
        private DateTime publishedAt, editAt;
        private int id;
        private Subforum container;
        private Post inReplyTo;
        private Dictionary<int, Post> replies;

        public Post (String title, String content,int id, User author, DateTime now,Post inReplyTo,Subforum container,DateTime editAt)
        {
            this.title = title;
            this.content = content;
            this.id = id;
            this.publishedAt = now;
            this.inReplyTo = inReplyTo;
            this.author = author;
            this.container = container;
            this.editAt = editAt;

        }



        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public String Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
        public DateTime PublishedAt
        {
            get
            {
                return publishedAt;
            }
           
        }
        public DateTime EditAt
        {
            get
            {
                return editAt;
            }
            set
            {
                editAt = value;
            }
        }
        public User GetAuthor
        {
            get
            {
                return author;
            }
            
        }
        public Subforum Container
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }
        }
        public bool IsOriginal()
        {
            return inReplyTo == null;
        }
        public void RemoveReply (int id)
        {
            replies.Remove(id);
        }
        public void AddReply(Post reply)
        {
            replies.Add(reply.Id, reply);
        }
        public Post [] GetAllReplies()
        {
            Post[] replyArr = new Post[replies.Values.Count];
            replies.Values.CopyTo(replyArr, 0);
            return replyArr;
        }
        public Post GetReply (int id)
        {
            Post reply;
            replies.TryGetValue(id, out reply);
            return reply;
        }
















    }
    

}