using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Admin
    {
        private DAL myDal;
        private User user;
        private Dictionary<int, Moderator> appointedMods;
        private Forum myForum;
        private int id;

        public Admin(User user, Forum myForum,DAL myDal,int id)
        {
            this.user = user;
            this.myForum = myForum;
            this.appointedMods = new Dictionary<int, Moderator>();
            this.myDal = myDal;
            this.id = id;

        }


        public User InnerUser
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
                return id;
            }
            set
            {
                id = value;
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
