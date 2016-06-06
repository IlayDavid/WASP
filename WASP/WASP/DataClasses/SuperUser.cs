using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Config;

namespace WASP.DataClasses
{
    public class SuperUser
    {

        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static SuperUser Get(int id)
        {
            if (Settings.UseCache())
                return WASP.Config.Settings.GetCache().GetSuperUser(id);
            return dal.GetSuperUser(id);
        }
        public static SuperUser[] Get(int[] ids)
        {
            return dal.GetSuperUsers(ids);
        }
        public SuperUser Update()
        {
            return dal.UpdateSuperUser(this);
        }

        public SuperUser Create()
        {
            return dal.CreateSuperUser(this);
        }

        public bool Delete()
        {
            return dal.DeleteSuperUser(this.Id);
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
