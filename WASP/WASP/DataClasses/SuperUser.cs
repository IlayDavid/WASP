using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class SuperUser
    {

        public static DAL2 dal = WASP.Config.Settings.GetDal();

        public static SuperUser Get(int id)
        {
            return dal.GetSuperUser(id);
        }

        public SuperUser ( int id, string userName, string password)
        {
            Id = id;
            Username = userName;
            Password = password;
        }
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        
    }
}
