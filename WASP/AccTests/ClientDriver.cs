namespace AccTests.Bridges.General.ClientSide
{
    public class ClientDriver
    {
        public static WASPClientBridge getBridge()
        {
            var bridge = new ClientProxyBridge(new ClientRealBridge());
            return bridge;
        }
    }
}
