using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UniVRPNity;
using UniVRPNity.Device;

public class ExampleWiimote : MonoBehaviour
{
    public string deviceName = "WiiMote0";
    public string deviceIP = "localhost";

    public AnalogRemote analog;
    public ButtonRemote button;

    //Apply the script on this transform
    public Transform transformHandled;

    public Vector3 sensibilityTranslation = new Vector3(10F, 10F, 10F);
    public Vector3 sensibilityRotation = new Vector3(100F, 100F, 100F);

    public Wiimote.Button forwardKey = Wiimote.Button.TOP;
    public Wiimote.Button backwardKey = Wiimote.Button.BOTTOM;
    public Wiimote.Button leftKey = Wiimote.Button.LEFT;
    public Wiimote.Button rightKey = Wiimote.Button.RIGHT;

    public bool forward = false;
    public bool backward = false;
    public bool left = false;
    public bool right = false;

    public bool nunchuk = true;

    public double angleJoystick = 0;
    public float intensityJoystick = 0;

    public void Start()
    {
        analog = new AnalogRemote(deviceName + '@' + deviceIP);
        analog.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(this.AnalogChanged);
        button = new ButtonRemote(deviceName + '@' + deviceIP, 18);
    }

    public void Reset()
    {
        transformHandled = transform;
    }

    public void Update()
    {
        analog.Mainloop();
        button.Mainloop();

        if (transformHandled != null)
        {
            if (forward)
                transformHandled.Translate(0, 0, sensibilityTranslation.z * Time.deltaTime, Space.Self);
            if (backward)
                transformHandled.Translate(0, 0, -sensibilityTranslation.z * Time.deltaTime, Space.Self);
            if (left)
                transformHandled.Translate(-sensibilityTranslation.x * Time.deltaTime, 0, 0, Space.Self);
            if (right)
                transformHandled.Translate(sensibilityTranslation.x * Time.deltaTime, 0, 0, Space.Self);

            if (nunchuk)
            {
                transformHandled.Rotate(0, (float)Math.Sin(Math.PI * angleJoystick / 180) * intensityJoystick * sensibilityRotation.y * Time.deltaTime, 0, Space.World);
                transformHandled.Rotate(-(float)Math.Cos(Math.PI * angleJoystick / 180) * intensityJoystick * sensibilityRotation.x * Time.deltaTime, 0, 0, Space.Self);
            }
        }
    }

    /* Analog event handler */
    private void AnalogChanged(AnalogEvent e)
    {
        angleJoystick = analog.LastEvent.Channel(19);
        intensityJoystick = (float)analog.LastEvent.Channel(20);
        if (intensityJoystick < 0.05)
            intensityJoystick = 0;

        forward = button.GetButtonState((int)(object)forwardKey);
        backward = button.GetButtonState((int)(object)backwardKey);
        left = button.GetButtonState((int)(object)leftKey);
        right = button.GetButtonState((int)(object)rightKey);
    }
}