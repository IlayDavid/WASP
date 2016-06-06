using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Reports
{
    public class ModeratorReport
    {
        private Moderator[] mods;
        private Forum forum;
        public ModeratorReport() { }
        public ModeratorReport(Forum forum, Moderator[] mods)
        {
            this.mods = mods;
            this.forum = forum;
        }

        public Moderator[] Mods
        {
            get { return mods; }
        }

        public Dictionary<string, dynamic> toJson()
        {
            Dictionary<string, dynamic> json = new Dictionary<string,dynamic>();
            
            json.Add("forum", forum.Id);
            List<Dictionary<string, dynamic>> moderators = new List<Dictionary<string, dynamic>>();
            foreach (Moderator mod in mods)
            {
                Dictionary<string, dynamic> entry = new Dictionary<string, dynamic>();
                entry.Add("id", mod.Id);
                entry.Add("appointer", mod.Appointer.Id);
                entry.Add("subforum", mod.SubForum.Id);
                entry.Add("startdate", mod.StartDate);
                List<Dictionary<string, dynamic>> posts = new List<Dictionary<string,dynamic>>();
                foreach(Post post in mod.User.GetAllPosts()){
                    Dictionary<string, dynamic> p = new Dictionary<string,dynamic>();
                    p.Add("postid", post.Id);
                    p.Add("publishedat", post.PublishedAt);
                    p.Add("title", post.Title);
                    p.Add("content", post.Content);
                    p.Add("subforum", post.Subforum);
                    posts.Add(p);
                }
                entry.Add("posts", posts);
                entry.Add("username", mod.User.Username);
                moderators.Add(entry);
            }
            json.Add("moderators", moderators);
            return json;
        }
    }
}
