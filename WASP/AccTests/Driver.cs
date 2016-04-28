using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccTests
{
    public class Driver
    {
        public static WASPBridge getBridge()
        {
            ProxyBridge bridge = new ProxyBridge(new RealBridge());
            return bridge;
        }
    }
}
