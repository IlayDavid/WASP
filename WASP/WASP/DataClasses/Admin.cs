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
        private Moderator modMe;
        private Dictionary<int, Moderator> appointedMods;
        private Forum myForum;


        public Admin(Moderator mod, Forum myForum,DAL myDal)
        {
            this.modMe = mod;
            this.myForum = myForum;
            this.appointedMods = new Dictionary<int, Moderator>();
            this.myDal = myDal;
        }


        public Moderator InnerMod
        {
            get
            {
                return modMe;
            }
            set
            {
                modMe = value;
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
