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
        int AddMember(Member m);
        int AddMessage(Message m);
        int AddMessage(Message m, Member member);
        int AddPost(Post p);
        int AddSubforum(Subforum sf);

        //delete data classes from data base
        int DeleteUser(string username);
        int DeleteMessage(Member member, int messageID);
        int DeletePost(int postID, Subforum sf);
        int DeleteSubforum(int subforumID);

        //get data classes from data base
        Member GetUser(string username);
        Message GetMessage(Member member, int messageID);
        Post GetThread(int postID, Subforum sf);
        Post GetPost(int postID, Subforum sf);
        Subforum GetSubforum(int subforumID);
    }
}
