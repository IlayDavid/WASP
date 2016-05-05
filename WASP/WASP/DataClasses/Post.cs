using System;
using System.Collections.Generic;

namespace WASP.DataClasses
{
    public class Post
    {
        private String title, content;
        private User author;
        private DateTime publishedAt, editAt;
        private int id;
        private Subforum subforum;
        private Post inReplyTo;
        private Dictionary<int, Post> replies= new Dictionary<int, Post>();
        private DAL dal;

        public Post(int id, String title, String content, User author, DateTime now, Post inReplyTo, Subforum subforum, DateTime editAt, DAL myDal)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.publishedAt = now;
            this.inReplyTo = inReplyTo;
            this.author = author;
            this.subforum = subforum;
            this.editAt = editAt;
            this.dal = myDal;
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
        public Subforum Subforum
        {
            get
            {
                return subforum;
            }
            set
            {
                subforum = value;
            }
        }

        public Post InReplyTo
        {
            get
            {
                return inReplyTo;
            }
            set
            {
                inReplyTo = value;
            }
        }
        public bool IsOriginal()
        {
            return inReplyTo == null;
        }
        public void RemoveReply(int id)
        {
            replies.Remove(id);
        }
        public void AddReply(Post reply)
        {
            replies.Add(reply.Id, reply);
        }
        public Post[] GetAllReplies()
        {
            Post[] replyArr = new Post[replies.Values.Count];
            replies.Values.CopyTo(replyArr, 0);
            return replyArr;
        }
        public Post GetReply(int id)
        {
            Post reply;
            replies.TryGetValue(id, out reply);
            return reply;
        }
        public void Delete()
        {
            string notificationMessage = String.Format("Post {0} deleted.", Id);
            //NotifyRepliers(new Notification(notificationMessage, true, GetAuthor, null));
            Post[] replies = GetAllReplies();
            foreach (Post reply in replies)
            {
                reply.Delete();
                RemoveReply(reply.Id);
            }
            this.author.RemovePost(this.id);
        }

        public void NotifyRepliers(Notification notification)
        {
            foreach (Post reply in GetAllReplies())
            {
                User target = reply.GetAuthor;
                //target.NewNotification(new Notification(notification.Message, notification.IsNew, 
                //    notification.Source, target));
            }
        }

        public int NumOfReplies()
        {
            int count = 0;
            foreach(Post reply in this.GetAllReplies())
            {
                count += 1 + reply.NumOfReplies();
            }
            return count;
        }
    }
}


