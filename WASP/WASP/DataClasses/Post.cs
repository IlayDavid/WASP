using System;
using System.Collections.Generic;
using System.Linq;

namespace WASP.DataClasses
{
    public class Post
    {
        private static int _idCounter = 0;
        private String _title, _content;
        private Member _author;
        private DateTime _publishedAt, _editAt;
        private int _id;
        private Subforum _container;
        private Post _inReplyTo;
        private List<Post> _replies;

        public Post (String title, String content, Member author, DateTime now ,Subforum container)
        {
            // container.AddThread(this);
            _title = title;
            _content = content;
            _id=_idCounter;
            _idCounter++;
            _publishedAt = now;
            _inReplyTo = null;
            _author = author;
            _container = container;
            _editAt = now;
            _replies = new List<Post>();

        }
        public static bool isValidOpening(String title, String content, Member author, DateTime now, Subforum container)
        {
            return !(Helper.isEmptyString(title) || Helper.isEmptyString(content)
                || author == null || now == null || container == null);
        }
        public Post(String content, Member author, DateTime now, Post inReplyTo)
        {
            // InReplyTo.AddReply(this);
            _title = inReplyTo._title;
            _content = content;
            _id = _idCounter;
            _idCounter++;
            _publishedAt = now;
            _inReplyTo = inReplyTo;
            _author = author;
            _container = inReplyTo.Container;
            _editAt = now;
            _replies = new List<Post>();
        }
        public static bool isValidReply(String content, Member author, DateTime now, Post inReplyTo)
        {
            return !( Helper.isEmptyString(content) || author == null
                || now == null || inReplyTo == null);
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public String Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }
        public DateTime PublishedAt
        {
            get
            {
                return _publishedAt;
            }
           
        }
        public DateTime EditAt
        {
            get
            {
                return _editAt;
            }
            set
            {
                _editAt = value;
            }
        }
        public Member GetAuthor
        {
            get
            {
                return _author;
            }
            
        }
        public Subforum Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public Post InReplyTo
        {
            get
            {
                return _inReplyTo;
            }
            set
            {
                _inReplyTo = value;
            }
        }
        public bool IsOriginal()
        {
            return _inReplyTo == null;
        }
        public void RemoveReply (Post post)
        {
            _replies.Remove(post);
        }
        public void AddReply(Post reply)
        {
            _replies.Add(reply);
        }
        public List<Post> GetAllReplies()
        {
            return _replies;
        }
        public Post GetReply (int id)
        {
            return _replies.First((x)=>x.Id==id);
        }
       

















    }
    

}