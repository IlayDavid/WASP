using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class DAL : IDAL
    {
        private List<Forum> _forums;
        public DAL()
        {
            _forums = new List<Forum>();
        }
        public int AddForum(Forum f)
        {
            _forums.Add(f);
            return 1;
        }

        public int DeleteForum(int forumID)
        {
            _forums.Remove(_forums.First(forum => forum.Id == forumID));
            return 1;
        }

        public Forum GetForum(int forumID)
        {
            return _forums.First(forum => forum.Id == forumID);
        }
    }
}
