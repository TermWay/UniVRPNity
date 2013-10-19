using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Vrpn;

namespace UniVRPNity
{
    /// <summary>
    /// Store related information about remote :
    /// <ul>
    /// <li>Name of the remote device.</li>
    /// <li>Type of device (button, analog, tracker).</li>
    /// <li>Socket to communicate with client.</li>
    /// </ul>
    /// </summary>
    class VrpnObjectInfo
    {
        /// <summary>
        /// Remote device of VrpnNet.
        /// </summary>
        public IVrpnObject Remote;

        /// <summary>
        /// Name of the device. Do not contain address.
        /// </summary>
        public string Name;

        /// <summary>
        /// Type of the device (button, analog, tracker).
        /// </summary>
        public UniVRPNity.Type Type;

        /// <summary>
        /// Socket from the client.
        /// </summary>
        public Socket Socket;

        /// <summary>
        /// Build a object without the socket.
        /// </summary>
        /// <param name="name">See <see cref="VrpnObjects.Name"/>.</param>
        /// <param name="type">See <see cref="VrpnObjects.Type"/>.</param>
        public VrpnObjectInfo(string name, UniVRPNity.Type type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
