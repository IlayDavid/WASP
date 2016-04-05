﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace WASP.DataClasses
{
    public class Member : User
    {
        private bool _isActive=false;
        private List<Post> posts= new List<Post>();
        private List<Message> messages = new List<Message>();
        public Forum MemberForum { get; private set; }
        public Member(String userName, String name, String email, String pass, Forum memberForum)
        {
            Name = name;
            UserName = userName;
            Email = email;
            Password = pass;
            MemberForum = memberForum;
        }

        public void confirmMail()
        {
            _isActive = true;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public List<Post> GetAllPosts()
        {
            return posts;
        }

        public Post GetPost(int postId)
        {
            return posts.First((x) => x.Id == postId);
        }
        public void DeletePost (Post post)
        {
            posts.Remove(post);
        }
        public void AddPost (Post post)
        {
            posts.Add(post);
        }

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }
        public void DeleteMessage(int messageID)
        {
            messages.Remove(messages.First(message => message.Id == messageID));
        }

        internal Message GetMessage(int messageID)
        {
            return messages.First((x) => x.Id == messageID);
        }
    }
}