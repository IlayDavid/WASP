using System;
using System.Collections.Generic;

namespace WASP
{
    public class Forum
    {
        internal int id;
        internal Dictionary<int, Subforum> subforums;


        public Forum()
        {

        }


        internal Subforum getSubForum(int sf_ID)
        {
            throw new NotImplementedException();
        }

        internal void addSubForum(Subforum sf)
        {
            throw new NotImplementedException();
        }

        internal bool isManager(int user_ID)
        {
            throw new NotImplementedException();
        }

        internal Thread findThread(int thread_ID)
        {
            throw new NotImplementedException();
        }
    }
}