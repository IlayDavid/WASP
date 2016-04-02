using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP
{
    public class User
    {
        private static bool hasSuperman = false;
        private bool isSuperMan;
        private String name;
        private String userName;
        private String email;
        private String pass;
        private int id;
        private Dictionary<int, Post> posts;


        public User(int id,bool isSuperMan, String name, String userName, String email, String pass)
        {
            this.id = id;
            if (!hasSuperman && isSuperMan)
            {
                this.isSuperMan = true;
                hasSuperman = true;
            }
            else if (hasSuperman && isSuperMan)
            {
                throw new Exception("attempted to create a new SuperUser");
            }
            else
            {
                this.isSuperMan = false;
            }
            this.name = name;
            this.userName = userName;
            this.email = email;
            this.pass = pass;
        }

        public String Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
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

        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public bool IsSuperMan
        {
            get
            {
                return isSuperMan;
            }
        }

        public String Password
        {
            get
            {
                return pass;
            }
            set
            {
                pass = value;
            }
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
        public void DeletePost (int post_ID)
        {
            posts.Remove(post_ID);
        }
        public void AddPost (Post post)
        {
            posts.Add(post.Id, post);
        }


    }
}