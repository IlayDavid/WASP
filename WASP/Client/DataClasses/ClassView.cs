using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DataClasses
{
    public class ForumView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static List<ForumView> getView(List<Forum> forums)
        {
            List<ForumView> ret = new List<ForumView>();
            foreach (Forum f in forums)
            {
                ret.Add(new ForumView() { ID = f.ID, Name = f.Name, Description = f.Description });
            }
            return ret;
        }
    }

    public class SubForumView
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PostView
    {
        public string _title { get; set; }
        public string _content { get; set; }
        public User _author { get; set; }
        public DateTime _publishedAt { get; set; }
        public DateTime _editAt { get; set; }
    }

    public class MessageView
    {
        public string _content { get; set; }
        public string _title { get; set; }
    }
    
}
