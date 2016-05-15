using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Reports
{
    public class ModeratorReport
    {
        private Moderator[] mods;
        public ModeratorReport() { }
        public ModeratorReport(Moderator[] mods)
        {
            this.mods = mods;
        }

        public Moderator[] Mods
        {
            get { return mods; }
        }

        public string toJson()
        {
            return "";
        }
    }
}
