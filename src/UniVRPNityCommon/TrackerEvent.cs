using System;
using System.Runtime.Serialization;

using UniVRPNityUtils;

namespace UniVRPNity
{
    [Serializable]
    /// <summary>
    /// Store information about a button pressed or release.
    /// <ul>
    ///     <li>The date of the event.</li>
    ///     <li>Which sensor is reporting.</li>
    ///     <li>Position of the tracker.</li>
    ///     <li>Orientation of the tracker.</li>
    /// </ul>
    /// </summary>
    public class TrackerEvent : Event
    {
        /// <summary>
        /// Which sensor is reporting.
        /// </summary>
        public int Sensor;

        /// <summary>
        /// Position of the sensor.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Orientation of the sensor.
        /// </summary>
        public Quaternion Orientation;

        public TrackerEvent() 
        {
            Device = "";
            this.Sensor = 0;
            this.Position = new Vector3();
            this.Orientation = new Quaternion();
        }

        public TrackerEvent(string device) :
            this()
        {
            Device = device;
        }

        public TrackerEvent(string device, Vector3 position, Quaternion orientation)
        {
            Device = device;
            this.Position = position;
            this.Orientation = orientation;
        }

        public override string ToString()
        {
            return "Tracker " + Sensor +
                " Position(" + Position.ToString() + ")" +
                " Orientation(" + Orientation.ToString() + ")" +
                base.ToString();
        }

        public override Type GetHandlerType()
        {
            return Type.Tracker;
        }
    }
}
