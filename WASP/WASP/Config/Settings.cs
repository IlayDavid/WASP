using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.Config
{
    static class Settings
    {
        private static DAL2 dal = null;

        public static DAL2 GetDal()
        {
            if (dal == null)
            {
                dal = new DALSQL();
            }
            return dal;
        }
    }
}
