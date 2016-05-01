using Client.BusinessLogic;
using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GUI
{
    public class Session
    {
        public static IBL bl = null;
        public static User user = null;
        public static Forum forum = null;
        public static Subforum subForum = null;
        public static Post post = null;
        public static INotificable currentWindow = null;
    }
}
