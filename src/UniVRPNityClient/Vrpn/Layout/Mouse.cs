using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity.Device
{
    public partial class Mouse : Remote
    {
        /// <summary>
        /// Mouse layout for click (VRPN).
        /// </summary>
        public enum Button
        {
            LEFT,
            MIDDLE,
            RIGHT,
            NONE
        }

        /// <summary>
        /// Mouse layout for movement (VRPN).
        /// </summary>
        public enum Analog
        {
            X,
            Y
        }
    }
}
