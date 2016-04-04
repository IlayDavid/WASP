using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP;
using WASP.DataClasses;

namespace AccTests.Tests
{
    public class Functions
    {
        public static SuperUser InitialSystem(WASPBridge proj)
        {
            return proj.initialize("SuperUser", "Moshe", "moshe@post.bgu.ac.il", "moshe123");
        }

        public static Forum CreateSpecForum(WASPBridge proj, SuperUser supervisor)
        {
            return proj.createForum(supervisor, "start-up", "ideas", "admin",
                                            "david", "david@post.bgu.ac.il", "david123");
        }
    }
}
