using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    class CL_Classes
    {
        public CL_Classes()
        {

        }
    }

    public class CLUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public CLUser()
        {
        }
    }

    public class CLNotification
    {
        public int id { get; set; }
        public string message { get; set; }
        public bool isnew { get; set; }
        public string sourceid { get; set; }
        public string targetid { get; set; }
        public CLNotification()
        {
        }
    }

    public class CLFriend
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public CLFriend()
        {
        }
    }

    public class CLPost
    {
        public string title { get; set; }
        public string content { get; set; }
        public int authorid { get; set; }
        public int container { get; set; }
        public int replypostid { get; set; }
        public int postid { get; set; }
        public CLPost()
        {

        }
    }

    public class CLForum
    {
        public string name { get; set; }
        public string description { get; set; }
        public int adminid { get; set; }
        public int forumid { get; set; }
        public CLForum()
        {

        }
    }

    public class CLSubforum
    {
        public string name { get; set; }
        public string description { get; set; }
        public int subforumid { get; set; }
        public CLSubforum()
        {

        }
    }

    public class CLModerator
    {
        public int subforumid { get; set; }
        public int appointedbyid { get; set; }
        public int moderatorid { get; set; }
        public CLModerator()
        {

        }
    }


}
