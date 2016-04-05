using System;
using System.Collections.Generic;
using WASP.DataClasses;
using WASP.DataClasses.Policies;
namespace WASP.Domain
{
    interface IBL
    {
        SuperUser initialize(String name, String userName, String email, String pass);
        ForumIBL getForumIBL(Forum memberForum);
        Forum getForum(Member member, int forumId);
        Forum createForum(SuperUser creator, String forumName, String description, String userName, String adminName, String email, String pass, Policy policy);
        List<Forum> getAllForums();
        SuperUser login(string username, string password);
    }
}
