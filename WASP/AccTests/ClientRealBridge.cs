using System;
using System.Collections.Generic;
using Client.BusinessLogic;
using Client.DataClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccTests
{
    public class ClientRealBridge : WASPClientBridge
    {
        private IBL _clientAPI;

        public ClientRealBridge()
        {
            _clientAPI = new BL();
        }
        
    }
}
