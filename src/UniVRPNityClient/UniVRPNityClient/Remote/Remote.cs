using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UniVRPNity
{
    [Serializable]
    public abstract class Remote
    {
        /// <summary>
        /// Name of the peripheral.
        /// </summary>
        private string name;

        /// <summary>
        /// Client which receive the informations of the VRPN server through the intermediate server.
        /// </summary>
        protected MiddleVRPNClient middleVRPNClient;


        public Remote(string name,
            string addressServer = Network.UniVRPNityServerDefaultAddress,
            int port = Network.UniVRPNityServerDefaultPort)
        {
            this.Name = name;
            middleVRPNClient = new MiddleVRPNClient(this, addressServer);
            middleVRPNClient.Receive();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public override bool Equals(object obj)
        {
            Remote r = (Remote) obj;
            return this.Name == r.Name &&
                   this.GetHandlerType() == r.GetHandlerType();
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.GetHandlerType().GetHashCode();
        }

        /// <summary>
        /// Fetch the events of the bufferEvent of <see cref="Remote.uniVRPNityClient"/> and spread it.
        /// </summary>
        public abstract void Mainloop();

        public abstract UniVRPNity.Type GetHandlerType();

        public void Quit()
        {
            middleVRPNClient.Quit();
        }
    }
}
