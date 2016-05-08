using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Exceptions;
namespace WASP.DataClasses
{
    public class Policy
    {
        public enum PostDeletePolicy : byte
        {
            Owner = 1, Moderator = 2, OwnerAndModerator = 3, Admin = 4, OwnerAndAdmin = 5, ModeratorAndAdmin = 6, OwnerModeratorAndAdmin = 7
        }
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static Policy Get(int id)
        {
            return dal.GetPolicy(id);
        }
        public static Policy[] Get(int[] ids)
        {
            throw new NotImplementedException("");
        }
        public Policy Create()
        {
            return dal.CreatePolicy(this);
        }

        public Policy Update()
        {
            return dal.UpdatePolicy(this);
        }

        public bool Delete()
        {
            return dal.DeletePolicy(id);
        }

        private int id;
        //post
        private  PostDeletePolicy deletePost;
        //security
        private  TimeSpan passwordPeriod;
        private  bool emailVerification;
        //moderator
        private  TimeSpan minimumSeniority;
        //stress
        private  int usersLoad;

        public TimeSpan PasswordTimeSpan
        {
            get { return passwordPeriod; }
            set { passwordPeriod = value; }
        }

        public TimeSpan MinimumSeniority
        {
            get { return minimumSeniority; }
            set { minimumSeniority = value; }
        }

        public bool EmailVerfication
        {
            get { return emailVerification; }
            set { emailVerification = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int UsersLoad
        {
            get { return usersLoad; }
            set { usersLoad = value; }
        }
        public PostDeletePolicy SelectedPostDeletePolicy
        {
            get { return deletePost; }
            set { deletePost = value; }
        }

        public Policy(int id, PostDeletePolicy deletePost, TimeSpan passwordPeriod, bool emailVerification, TimeSpan minimumSeniority, int usersLoad)
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

        public bool Validate(User member)
        {
            if (DateTime.Now - member.PasswordChangeDate > passwordPeriod)
                member.NewNotification(new Notification(-1, "Password too old.", true, null, null));
                //throw new WASP.Exceptions.PolicyException("Password too old.");
            return true;
        }

        public bool Validate(Moderator mod)
        {
            if (DateTime.Now - mod.User.StartDate < minimumSeniority)
            {
                throw new PolicyException("");
            }
            return true;
        }

        public bool CanDeletePost(Authority.Level level, bool owner = false)
        {
            bool ownerCan = ((byte)Authority.Level.User & (byte)SelectedPostDeletePolicy) != 0;
            if (owner && ownerCan)
                return true;

            // If not owner but still can:
            if (((byte)level & (byte)(SelectedPostDeletePolicy)) != 0)
                return true;
                
            return false;
        }
    }
}

