using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.DataClasses.Cache2
{
    interface IDALCache2
    {
        void setPostExpiration(int hours, int minutes, int secs);

        void AddForum(Forum f);
        void AddSubforum(Subforum sf);
        void AddSuperUser(SuperUser su);
        void AddUser(User user);
        void AddModerator(Moderator mod);
        void AddAdmin(Admin admin);
        void AddPost(Post post);

        void RemoveForum(int f);
        void RemoveSubforum(int sf);
        void RemoveSuperUser(int su);
        void RemoveUser(int user, int forum);
        void RemoveModerator(int mod, int subforum);
        void RemoveAdmin(int admin, int forum);
        void RemovePost(int p);

        Forum GetForum(int f);
        Subforum GetSubforum(int sf);
        SuperUser GetSuperUser(int su);
        User GetUser(int user, int forum);
        Moderator GetModerator(int mod, int subforum);
        Admin GetAdmin(int admin, int forum);
        Post GetPost(int p);

    }
}
