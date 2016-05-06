using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Policy
    {
        public enum PostDeletePolicy : byte
        {
            Owner = 1, Moderator = 2, OwnerAndModerator = 3, Admin = 4, OwnerAndAdmin = 5, ModeratorAndAdmin=6, OwnerModeratorAndAdmin=7
        }
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static Policy Get(int id)
        {
            return dal.GetPolicy(id);
        }

        private int id;
        //post
        private  int deletePost;
        //security
        private  TimeSpan passwordPeriod;
        private  bool emailVerification;
        //moderator
        private  TimeSpan minimumSeniority;
        //stress
        private  int usersLoad;

        public Policy(int deletePost, TimeSpan passwordPeriod, bool emailVerification, TimeSpan minimumSeniority, int usersLoad)
        {
            this.deletePost = deletePost;
            this.passwordPeriod = passwordPeriod;
            this.emailVerification = emailVerification;
            this.minimumSeniority = minimumSeniority;
            this.usersLoad = usersLoad;
        }

        bool CheckPassword(string password)
        {
            return true;
        }
    }
}

