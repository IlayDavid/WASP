using System.Collections.Generic;

namespace AccTests
{
    public class Forum
    {
        public Forum(string subject, User admin)
        {
            _subject = subject;
            _admin = admin;
        }

        //public int _forumId { get; set; }
        public string _subject{ get; set; }
        public User _admin{ get; set; }
        
        //private User _admins;
        //private List<UserThread> _threads;

        ////getter & setter for admins
        //public void addAdmin(User user)
        //{
        //    _admins.Add(user);
        //}
        //public void removeAdmin(User user)
        //{
        //    _admins.Remove(user);
        //}

        ////getter & setter for threads
        //public void addThread(UserThread thread)
        //{
        //    _threads.Add(thread);
        //}
        //public void removeThread(UserThread thread)
        //{
        //    _threads.Remove(thread);
        //}

    }
}