using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity
{
    [Serializable]
    public class AnalogRemote : Remote
    {
        public delegate void AnalogChangeEventHandler(AnalogEvent e);
        public AnalogChangeEventHandler AnalogChanged;

        /// <summary>
        /// Last event receive by the VRPN server.
        /// </summary>
        private AnalogEvent lastEvent;

        public AnalogRemote(string name,
                            string addressServer = Network.UniVRPNityServerDefaultAddress,
                            int port = Network.UniVRPNityServerDefaultPort) :
            base(name, addressServer, port)
        {
            AnalogChanged = new AnalogChangeEventHandler(Update);
            lastEvent = new AnalogEvent();
        }

        private void Update(AnalogEvent e)
        {
            lastEvent = e;
        }

        public override void Mainloop()
        {    
            Stack<Event> waitedEvent = middleVRPNClient.BufferEvent.FlushEvent();
         
            foreach (Event e in waitedEvent)
            {
                //Type must be compatible to be castable
                if (e.GetHandlerType() == this.GetHandlerType())
                {
                    AnalogEvent analogEvent = (AnalogEvent)e;
                    //Dispatch event to their listener
                    this.AnalogChanged(analogEvent);
                }
            }
            waitedEvent.Clear();
        }

        public override UniVRPNity.Type GetHandlerType()
        {
            return UniVRPNity.Type.Analog;
        }

        public override string ToString()
        {
            return base.ToString() + lastEvent.ToString();
        }

        public double Channel(int c)
        {
            return LastEvent.Channel(c);
        }

        public AnalogEvent LastEvent
        {
            get
            {
                return this.lastEvent;
            }
        }
    }
}
