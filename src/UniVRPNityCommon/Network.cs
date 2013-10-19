using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity
{
    /// <summary>
    /// Common network information. 
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Default address of the VRPN server.
        /// </summary>
        public const string VrpnServerDefaultAddress = "127.0.0.1";

        /// <summary>
        /// Default address of Middle VRPN server.
        /// </summary>
        public const string UniVRPNityServerDefaultAddress = "127.0.0.1";

        /// <summary>
        /// Default port of the middle vrpn server.
        /// </summary>
        public const int UniVRPNityServerDefaultPort = 8881;
    }
}
