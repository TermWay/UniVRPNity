using System;
using System.Runtime.Serialization;

namespace UniVRPNity
{
    /// <summary>
    /// Store information about a button pressed or release.
    /// <ul>
    ///     <li>The date of the event.</li>
    ///     <li>Which button is pressed.</li>
    ///     <li></li>
    /// </ul>
    /// </summary>
    [Serializable]
    public class ButtonEvent : Event
    {
        /// <summary>
        /// Which button is pressed.
        /// </summary>
        public int Button;

        /// <summary>
        /// State of the button : 
        /// - true : pressed (on).
        /// - false : released (off).
        /// </summary>
        public bool State;

        /// <summary>
        /// Build an empty button event with current time.
        /// </summary>
        public ButtonEvent()
        {
            Device = "";
            Button = 0;
            State = false;
            Time = DateTime.Now;
        }

        /// <summary>
        /// Build an empty button event with current time.
        /// </summary>
        /// <param name="device">Name of the device.</param>
        public ButtonEvent(string device) :
            this()
        {
            Device = device;
        }

        /// <summary>
        /// Build a complete button event with current time.
        /// </summary>
        /// <param name="device">Name of the device.</param>
        /// <param name="button">Which button is pressed.</param>
        /// <param name="state">If button is pressed.</param>
        public ButtonEvent(string device, int button, bool state)
        {
            Device = device;
            Button = button;
            State = state;
            Time = DateTime.Now;
        }

        /// <summary>
        /// Build a complete button event with current time.
        /// </summary>
        /// <param name="device">Name of the device.</param>
        /// <param name="button">Which button is pressed.</param>
        /// <param name="state">If button is pressed.</param>
        /// <param name="time">The time of the event.</param>
        public ButtonEvent(string device, int button, bool state, DateTime time) :
            this(device, button, state)
        {
            Time = time;
        }

        public override string ToString()
        {
            return "Button " + Button.ToString() + " " +
                ((State) ? "pressed" : "released") +
                base.ToString();
        }

        public override Type GetHandlerType()
        {
            return Type.Button;
        }
    }
}
