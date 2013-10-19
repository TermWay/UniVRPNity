using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity
{
    [Serializable]
    public class TrackerRemote : Remote
    {
        public delegate void TrackerChangeEventHandler(TrackerEvent e);
        public TrackerChangeEventHandler TrackerChanged;

        /// <summary>
        /// Last events receive for each sensors.
        /// </summary>
        private List<TrackerEvent> sensors;

        /// <summary>
        /// If no sensor number is given, the TrackerRemote has 10 buttons.
        /// </summary>
        public const int DefaultSensorsNumber = 16;

        public TrackerRemote(string name, 
                            int sensorsNumber = DefaultSensorsNumber,
                            string addressServer = Network.UniVRPNityServerDefaultAddress,
                            int port = Network.UniVRPNityServerDefaultPort) :
            base(name, addressServer, port)
        {
            this.sensors = new List<TrackerEvent>(sensorsNumber);
            for (int i = 0; i < sensorsNumber; i++)
            {
                sensors.Add(new TrackerEvent());
            }
            TrackerChanged = new TrackerChangeEventHandler(Update);
        }

        private void Update(TrackerEvent e)
        {
            if (e.Sensor < sensors.Capacity)
            {
                sensors[e.Sensor] = e;
            }
        }

        public override void Mainloop()
        {
            Stack<Event> waitedEvent = middleVRPNClient.BufferEvent.FlushEvent();

            foreach (Event e in waitedEvent)
            {
                //Type must be compatible to be castable
                if (e.GetHandlerType() == this.GetHandlerType())
                {
                    TrackerEvent trackerEvent = (TrackerEvent)e;
                    //Dispatch event to their listener
                    this.TrackerChanged(trackerEvent);
                }
            }
            waitedEvent.Clear();
        }

        public override UniVRPNity.Type GetHandlerType()
        {
            return UniVRPNity.Type.Tracker;
        }

        public override string ToString()
        {
            return base.ToString() + sensors.ToString();
        }

        public List<TrackerEvent> Sensors
        {
            get
            {
                return this.sensors;
            }
        }
    }
}
