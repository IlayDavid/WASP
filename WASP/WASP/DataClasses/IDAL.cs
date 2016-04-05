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
        int AddForum(Forum f);

        //delete data classes from data base
        int DeleteForum(int forumID);

        //get data classes from data base
        Forum GetForum(int forumID);
    }
}
