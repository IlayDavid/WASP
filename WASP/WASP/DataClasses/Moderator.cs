using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Moderator
    {
        private User me;
        private DateTime termExpiration;
        private Subforum subForum;
        private Admin appointer;
        private int id;
        private DAL dal;

        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer, int id,DAL dal)
        {
            this.id = id;
            this.me = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.dal = dal;
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

        public User user
        {
            get
            {
                return me;
            }
            set
            {
                me = value;
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
