using System;
using System.Collections.Generic;

namespace WASP
{
    public class Thread
    {
        Dictionary<int, Post> posts;
        Post openingPost;
        
        internal Thread(Post openPost)
        {
            posts = new Dictionary<int, Post>();
            openingPost = openPost;
        }


        internal void addPost(Post post)
        {
            throw new NotImplementedException();
        }

        internal void deletePost(int post_ID)
        {
            throw new NotImplementedException();
        }
    }
}