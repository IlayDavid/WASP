using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public interface ForumIDAL
    {
        //add data classes to data base
        Forum GetForum();
        int AddUser(User u);
        int AddMessage(Message m);
        int AddMessage(Message m, Member member);
        int AddPost(Post p);
        int AddSubforum(Subforum sf);

        //delete data classes from data base
        int DeleteUser(string username);
        int DeleteMessage(int messageID);
        int DeletePost(int postID);
        int DeleteSubforum(int subforumID);

        //get data classes from data base
        User GetUser(string username);
        Message GetMessage(int messageID);
        Post GetThread(int postID);
        Post GetPost(int postID);
        Subforum GetSubforum(int subforumID);
    }
}
