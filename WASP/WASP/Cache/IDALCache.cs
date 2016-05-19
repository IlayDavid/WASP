using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.Cache
{
    interface IDALCache
    {
        void setPostExpiration(int hours, int minutes, int secs);

        void AddForum(Forum f);
        void AddSubforum(Subforum sf);
        void AddUser(User user);
        void AddModerator(Moderator mod);
        void AddAdmin(Admin admin);
        void AddPost(Post post);

        void RemoveForum(int f);
        void RemoveSubforum(int sf);
        void RemoveUser(int user);
        void RemoveModerator(int mod);
        void RemoveAdmin(int admin);
    }
}
