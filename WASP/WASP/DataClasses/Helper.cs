using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public static class Helper
    {
        public static bool isEmptyString(String str)
        {
            return (str == null || str.Equals(""));
        }

        public static bool isTermValid(DateTime term)
        {
            return term != null && (term.CompareTo(DateTime.Now) > 0);
        }
    }
}
