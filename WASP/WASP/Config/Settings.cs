using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;
using WASP.Cache;

namespace WASP.Config
{
    static class Settings
    {
        private static DAL2 dal = null;
        private static IDALCache cache = null;
        public static DAL2 GetDal()
        {
            if (dal == null)
            {
                dal = new DALSQL();
            }
            return dal;
        }

        public static bool UseCache()
        {
            return false;
        }

        public static IDALCache GetCache()
        {
            if (cache == null)
                cache = new DALCache(GetDal());
            return cache;
        }

        public static void NotificationMethod(int uId, int fId)
        {
            WASP.Server.NotificationServer.GroupNotify(WASP.Server.NotificationServer.GetGroup(uId, fId), "ntf");
        }
    }
}
