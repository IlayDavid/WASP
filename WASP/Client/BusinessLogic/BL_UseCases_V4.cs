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
        public void restorePasswordbyAnswers(int userid, int forum_id, List<string> answers, string newPassword)
        {
            _logger.writeToFile("restore password by answers");
            _cl.restorePasswordbyAnswers(userid, forum_id, answers, newPassword);
        }
        //---------------------------Version 4 Use Cases End------------------------------------

    }
}
