﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.DAL_EXCEPTIONS
{
    class InvalidException : Exception
    {
        public InvalidException(String errorMsg) : base (errorMsg)
        {
        }

    }
}
