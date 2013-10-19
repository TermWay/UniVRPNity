using System;
using UnityEngine;

namespace UniVRPNity.Device
{
    /// <summary>
    /// A specialized client for mouse using VRPN.
    /// 0 ------> 
    /// |       x
    /// |
    /// V  y
    /// <typeparam name="Action"></typeparam>
    [Serializable]
    public partial class Mouse : Remote
    {
        public AnalogRemote Movement;
        public ButtonRemote Click;

        public Vector2 Coordinates;
        public Vector2 LastCoordinates;
        public Vector2 Diff;

        public override void Reset()
        {
            base.Reset();
            Name = "Mouse0";
        }

        public void Start()
        {
            Click = new ButtonRemote(Name + "@" + Address, 3);
            Click.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(ButtonChanged);
            Movement = new AnalogRemote(Name + "@" + Address);
            Movement.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(AnalogChanged);
        }

        public void Update()
        {
            Movement.Mainloop();
            Click.Mainloop();

            Diff = new Vector2(
                Coordinates.x - LastCoordinates.x,
                Coordinates.y - LastCoordinates.y);

            LastCoordinates.x = Coordinates.x;
            LastCoordinates.y = Coordinates.y;
        }

        public virtual void ButtonChanged(UniVRPNity.ButtonEvent e) { }

        public virtual void AnalogChanged(UniVRPNity.AnalogEvent e)
        {
            this.Coordinates.x = (float)e.Channel((int)Analog.X);
            this.Coordinates.y = (float)e.Channel((int)Analog.Y);
        }
    }
}