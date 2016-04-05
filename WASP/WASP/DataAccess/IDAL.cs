using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.DataAccess
{
    interface IDAL
    {
        //add data classes to data base
        int AddUser(User u);

        int AddForum(Forum f);

        int AddMessage(Message m);

        int AddPost(Post p);

        int AddSubforum(Subforum sf);

        //delete data classes from data base
        int DeleteUser(string username);

        int DeleteForum(int forumID);

        int DeleteMessage(int messageID);

        int DeletePost(int postID);

        int DeleteSubforum(int subforumID);

        //get data classes from data base
        User GetUser(string username);

        Forum GetForum(int forumID);

        Message GetMessage(int messageID);

        Post GetPost(int postID);

        Subforum GetSubforum(int subforumID);




    }
}
