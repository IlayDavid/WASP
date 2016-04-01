using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccTests
{
    public class Post
    {
        public Post (string content)
        {
            _content = content;
        }
        public string _content { set; get; }

    }
}