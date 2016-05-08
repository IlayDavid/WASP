using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.DAL_EXCEPTIONS
{
    class UpdateException : WASP.Exceptions.WaspException
    {
        public UpdateException(String errorMsg) : base(errorMsg)
        {
        }

    }
}
