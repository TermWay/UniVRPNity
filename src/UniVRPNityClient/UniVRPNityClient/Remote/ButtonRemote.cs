using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity
{
    [Serializable]
    public class ButtonRemote : Remote
    {
        public delegate void ButtonChangeEventHandler(ButtonEvent e);
        public ButtonChangeEventHandler ButtonChanged;

        /// <summary>
        /// State of all button. Init at false.
        /// </summary>
        public List<ButtonEvent> buttonStates;

        /// <summary>
        /// If no button number is given, the ButtonRemote has 256 buttons.
        /// </summary>
        public const int DefaultButtonNumber = 256;

        /// <summary>
        /// Build a button remote.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="buttonNumber"></param>
        public ButtonRemote(string name, 
                            int buttonNumber = DefaultButtonNumber,
                            string addressServer = Network.UniVRPNityServerDefaultAddress,
                            int port = Network.UniVRPNityServerDefaultPort) :
            base(name, addressServer, port)
        {
            buttonStates = new List<ButtonEvent>(buttonNumber);
            for (int i = 0; i < buttonNumber; i++)
            {
                buttonStates.Add(new ButtonEvent("", i, false));
            }
            ButtonChanged = new ButtonChangeEventHandler(Update);
        }

        private void Update(ButtonEvent e)
        {
            if (e.Button < buttonStates.Capacity)
            {
                buttonStates[e.Button] = e;
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
                    ButtonEvent buttonEvent = (ButtonEvent)e;
                    //Dispatch event to their listener
                    this.ButtonChanged(buttonEvent);
                }
            }
            waitedEvent.Clear();
        }

        /// <summary>
        /// Recover the buttons number.
        /// </summary>
        /// <returns>Number of buttons.</returns>
        public int GetNumButtons()
        {
            return this.buttonStates.Count;
        }

        public bool GetButtonState(int index)
        {
            return buttonStates[index].State;
        }

        public override UniVRPNity.Type GetHandlerType()
        {
            return UniVRPNity.Type.Button;
        }

        public override string ToString()
        {
            string str = "[ ";
            foreach (ButtonEvent e in buttonStates)
            {
                str += e + " ,";
            }
            str += "]";

            return str;
        }
    }
}
