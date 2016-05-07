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
        private Dictionary<int, Post> replies = null;
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static Post Get(int id)
        {
            return dal.GetPost(id);
        }

        public Post Create()
        {
            Post thisPost = dal.CreatePost(this);
            string notificationMessage = String.Format("Post {0} created.", thisPost.Id);
            Notification notif = new Notification(-1, notificationMessage, true, thisPost.GetAuthor, null);
            Subforum.Forum.NotifyAllMembers(notif);
            return thisPost;
        }

        public Post Update()
        {
            Post thisPost = dal.UpdatePost(this);
            string notificationMessage = String.Format("Post {0} updated.", thisPost.Id);
            NotifyRepliers(new Notification(-1, notificationMessage, true, thisPost.GetAuthor, null));
            return thisPost;
        }

        public bool Delete()
        {
            string notificationMessage = String.Format("Post {0} deleted.", Id);
            NotifyRepliers(new Notification(-1, notificationMessage, true, GetAuthor, null));
            Post[] replies = GetAllReplies();
            foreach (Post reply in replies)
            {
                reply.Delete();
                RemoveReply(reply.Id);
            }
            this.author.RemovePost(Id);
            return dal.DeletePost(Id);
        }

        public Post(int id, String title, String content, User author, DateTime now, Post inReplyTo, Subforum subforum, DateTime editAt)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.publishedAt = now;
            this.inReplyTo = inReplyTo;
            this.author = author;
            this.subforum = subforum;
            this.editAt = editAt;
        }

        // DEPRECATED
        public Post(int id, String title, String content, User author, DateTime now, Post inReplyTo, Subforum subforum, DateTime editAt, DAL2 dal)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.publishedAt = now;
            this.inReplyTo = inReplyTo;
            this.author = author;
            this.subforum = subforum;
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
        public string Title
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
                return this.inReplyTo;
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
            Replies.Remove(id);
        }
        public void AddReply(Post reply)
        {
            Replies.Add(reply.Id, reply);
        }
        
        private Dictionary<int, Post> Replies
        {
            get
            {
                if (replies == null)
                {
                    replies = new Dictionary<int, Post>();
                    foreach (Post reply in dal.GetReplies(this.id))
                    {
                        this.replies.Add(reply.id, reply);
                    }
                }

                return replies;
            }
        }

        public Post[] GetAllReplies()
        {
            
            Post[] replyArr = new Post[Replies.Values.Count];
            Replies.Values.CopyTo(replyArr, 0);
            return replyArr;
        }
        public Post GetReply(int id)
        {
            Post reply;
            Replies.TryGetValue(id, out reply);
            return reply;
        }
        

        public void NotifyRepliers(Notification notification)
        {
            foreach (Post reply in GetAllReplies())
            {
                User target = reply.GetAuthor;
                target.NewNotification(new Notification(-1, notification.Message, notification.IsNew, 
                    notification.Source, target));
            }
        }

        public int NumOfReplies()
        {
            int count = 0;
            foreach (Post reply in this.GetAllReplies())
            {
                count += 1 + reply.NumOfReplies();
            }
            return count;
        }
    }
}


