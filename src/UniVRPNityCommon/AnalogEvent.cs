using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniVRPNity
{
    [Serializable]
    /// <summary>
    /// Store information about a button pressed or release.
    /// <ul>
    ///     <li>The date of the event.</li>
    ///     <li>Which button is pressed.</li>
    ///     <li></li>
    /// </ul>
    /// </summary>
    public class AnalogEvent : Event
    {
        /// <summary>
        /// Analog values.
        /// </summary>
        public List<double> Channels;

        public AnalogEvent()
        {
            Device = "";
            this.Channels = new List<double>();
        }

        public AnalogEvent(string device) :
            this()
        {
            Device = device;
        }

        public AnalogEvent(string device, List<double> channels)
        {
            Device = device;
            this.Channels = channels;
        }

        public override string ToString()
        {
            string repr = "";
            foreach (double channel in Channels)
            {
                repr += channel + " ";
            }
            return Channels.Count + " chan " +
                "[" + repr + "]" + base.ToString();
        }

        /// <summary>
        /// Recover the channels number.
        /// </summary>
        /// <returns>Number of channels.</returns>
        public int getNumChannels()
        {
            return this.Channels.Count;
        }

        public Double Channel(int index)
        {
            if (index >= Channels.Count)
                return 0;
            return Channels[index];
        }

        public override Type GetHandlerType()
        {
            return Type.Analog;
        }
    }
}
