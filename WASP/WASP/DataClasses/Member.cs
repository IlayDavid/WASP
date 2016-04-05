using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP
{
    public class Member : User
    {
        
        private bool _isActive=false;
        private List<Post> posts= new List<Post>();
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
    }
}