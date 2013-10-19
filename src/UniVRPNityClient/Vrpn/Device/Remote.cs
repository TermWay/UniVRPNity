using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity.Device
{
    public class Remote
    {
        public string Name;
        public string Address;

        public virtual void Reset()
        {
            Address = "localhost";
        }
    }
}
