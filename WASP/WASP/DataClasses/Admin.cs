using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Admin
    {
        private static DAL2 dal = WASP.Config.Settings.GetDal();
        private User user;
        private Dictionary<int, Moderator> appointedMods = null;
        private Forum myForum;

        public static Admin Get(int adminId, int forumId)
        {
            return dal.GetAdmin(adminId, forumId);
        }

        public Admin Create()
        {
            return dal.CreateAdmin(this);
        }

        public Admin Update()
        {
            return dal.UpdateAdmin(this);
        }

        public bool Delete()
        {
            return dal.DeleteAdmin(Id, Forum.Id);
        }

        public Admin(User user, Forum myForum)
        {
            this.user = user;
            this.myForum = myForum;
        }

        // DEPRECATED
        public Admin(User user, Forum myForum, DAL2 dal)
        {
            this.user = user;
            this.myForum = myForum;
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public int Id
        {
            get
            {
                return User.Id;
            }

        }


        public Forum Forum
        {
            get
            {
                return myForum;
            }
            set
            {
                myForum = value;
            }


        }

        private Dictionary<int, Moderator> AppointedMods
        {
            get
            {
                if(appointedMods == null)
                {
                    appointedMods = new Dictionary<int, Moderator>();
                    foreach (Moderator mod in dal.GetAdminAppointedMods(Id,Forum.Id))
                    {
                        appointedMods.Add(mod.Id, mod);
                    }
                }
                return appointedMods;
            }
        }
        public Moderator[] GetAllAppointedMods()
        {
            Moderator[] modArr = new Moderator[appointedMods.Values.Count];
            appointedMods.Values.CopyTo(modArr, 0);
            return modArr;
        }

        public Moderator GetAppointedMods(int modId)
        {
            Moderator mod;
            appointedMods.TryGetValue(modId, out mod);
            return mod;
        }
        public void DeleteAppointedMod(int modId)
        {
            appointedMods.Remove(modId);
        }
        public void AddAppointedMod(Moderator mod)
        {
            appointedMods.Add(mod.Id, mod);
        }


    }
}
