using System.Collections.Generic;

namespace UniVRPNity.Device
{

    /// <summary>
    /// A specialized client for keyboard.
    /// </summary>
    /// <typeparam name="Action"></typeparam>
    public partial class Keyboard : Remote
    {
        public ButtonRemote Keys;
  
        public override void Reset()
        {
            base.Reset();
            Name = "Keyboard0";
        }

        public void Start()
        {
            Keys = new ButtonRemote(Name + "@" + Address, 256);
        }

        public void Update()
        {
            Keys.Mainloop();
        }
    }
}