using System;

namespace Client.DataClasses
{
    public class Moderator
    {

        public User user;
        public DateTime term;
        public User appointBy;
        
        public Moderator() { }
        public Moderator(User user, DateTime term, User admin)
        {
            this.user = user;
            this.term = term;
            appointBy = admin;
        }
    }
}