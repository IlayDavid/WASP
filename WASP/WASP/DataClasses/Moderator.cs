using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.Config;
namespace WASP.DataClasses
{
    public class Moderator : Authority
    {
        public override Authority.Level AuthorizationLevel()
        {
            return Authority.Level.Mod;
        }

        private User user;
        private DateTime termExpiration, startDate;
        private Subforum subForum;
        private Admin appointer;
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static Moderator Get(int modId, int subforumID)
        {
            if (Settings.UseCache())
                return WASP.Config.Settings.GetCache().GetModerator(modId, subforumID);
            return dal.GetModerator(modId, subforumID);
        }
        public static Moderator[] Get(int[] ids, int sfId)
        {
            return dal.GetModerators(ids, Subforum.Get(sfId));
        }

        public Moderator Create()
        {
            return dal.CreateModerator(this);
        }

        public Moderator Update()
        {
            return dal.UpdateModerator(this);
        }

        public bool Delete()
        {
            return dal.DeleteModerator(Id, SubForum.Id);
        }

        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.startDate = DateTime.Now;
        }
        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer, DateTime startDate)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.startDate = startDate;
        }

        // DEPRECATED
        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer, DAL2 dal)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.startDate = DateTime.Now;
        }

        // DEPRECATED
        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer, DateTime startDate, DAL2 dal)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.startDate = startDate;
        }

        public int Id
        {
            get
            {
                return User.Id;
            }
            set
            {
                User.Id = value;
            }
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

        public DateTime TermExp
        {
            get
            {
                return termExpiration;
            }
            set
            {
                termExpiration = value;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        public Subforum SubForum
        {
            get
            {
                return subForum;
            }
            set
            {
                subForum = value;
            }
        }

        public Admin Appointer
        {
            get
            {
                return appointer;
            }
            set
            {
                appointer = value;
            }
        }
    }
}
