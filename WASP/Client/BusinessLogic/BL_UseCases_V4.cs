using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public partial class BL : IBL
    {
        //---------------------------Version 4 Use Cases Start------------------------------------

        //login by client-session password (requested in ass3)
        public User loginBySession(string session)
        {
            return _cl.loginBySession(session);
        }
        public void restorePasswordbyAnswers(string username, List<string> answers, string newPassword)
        {
            throw new NotImplementedException();
        }
        //---------------------------Version 4 Use Cases End------------------------------------

    }
}
