﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.DAL_EXCEPTIONS
{
    class InvalidException : WASP.Exceptions.WaspException
    {
        public InvalidException(String errorMsg) : base (errorMsg)
        {
        }

    }
}
