using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniVRPNity.Device
{
    [Serializable]
    public class Layout
    {
        public DeviceType Type = DeviceType.Undefined;

        private Dictionary<DeviceType, System.Type> analogMap;
        private Dictionary<DeviceType, System.Type> buttonMap;

        public enum DeviceType
        {
            Undefined ,
            FrenchKeyboard,
            Mouse,
            Wiimote,
        }

        public Layout()
        {
            analogMap = new Dictionary<DeviceType, System.Type>();
            buttonMap = new Dictionary<DeviceType, System.Type>();
            this.initAnalog();
            this.initButton();
        }

        private void initAnalog()
        {
            analogMap[DeviceType.Mouse] = typeof(Mouse.Analog);
            analogMap[DeviceType.Wiimote] = typeof(Wiimote.Analog); 
        }

        private void initButton()
        {
            buttonMap[DeviceType.FrenchKeyboard] = typeof(Keyboard.FrenchKeys);
            buttonMap[DeviceType.Mouse] = typeof(Mouse.Button);
            buttonMap[DeviceType.Wiimote] = typeof(Wiimote.Button);
        }

        public System.Type GetAnalogEnum()
        {
            return (analogMap.ContainsKey(Type)) ? analogMap[Type] : null;
        }

        public System.Type GetButtonEnum()
        {
            return (buttonMap.ContainsKey(Type)) ? buttonMap[Type] : null;
        }

    }
}
