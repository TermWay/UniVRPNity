using System;
using System.Runtime.Serialization;

namespace UniVRPNity
{
    public enum Type { Analog, Button, Tracker };

    [Serializable]
    /// <summary>
    /// Base class for a event which contains the date of the event.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Name of the device.
        /// </summary>
        protected string device;

        /// <summary>
        /// Date of the event.
        /// </summary>
        protected DateTime time;

        /// <summary>
        /// Build a complete button event with current time.
        /// </summary>
        public Event()
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// Build a complete button event with a given time.
        /// </summary>
        public Event(DateTime time)
        {
            Time = time;
        }

        public string Device
        {
            get
            {
                return this.device;
            }
            set
            {
                this.device = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }

        public abstract Type GetHandlerType();

        public override string ToString()
        {
            return " at " + Time.ToString();
        }

        public static System.Type ConvertType(UniVRPNity.Type type)
        {
            switch (type)
            {
                case Type.Analog:
                    return typeof(AnalogEvent);
                case Type.Button:
                    return typeof(ButtonEvent);
                case Type.Tracker:
                    return typeof(TrackerEvent);
                default:
                    return null;
            }
        }
    }   
}
