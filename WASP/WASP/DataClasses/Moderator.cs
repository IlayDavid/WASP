﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Moderator
    {
        private User user;
        private DateTime termExpiration,startDate;
        private Subforum subForum;
        private Admin appointer;
        private DAL2 dal;


        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer, DAL2 dal)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.dal = dal;
            this.startDate = DateTime.Now;
        }
        public Moderator(User user, DateTime termExpiration, Subforum sf, Admin appointer,DateTime startDate ,DAL2 dal)
        {
            this.user = user;
            this.termExpiration = termExpiration;
            this.subForum = sf;
            this.appointer = appointer;
            this.dal = dal;
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
