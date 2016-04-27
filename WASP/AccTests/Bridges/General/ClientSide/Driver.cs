namespace AccTests.Bridges.General.ClientSide
{
    public class Driver
    {
        public static WASPBridge getBridge()
        {
            ProxyBridge bridge = new ProxyBridge();
            // add when real bridge is ready
            bridge.proj = new RealBridge();
            return bridge;
        }
    }
}
