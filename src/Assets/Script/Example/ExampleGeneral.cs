using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UniVRPNity;

public class ExampleGeneral : MonoBehaviour
{
    public string deviceName = "Mouse0";
    public string deviceIP = "localhost";

    public enum Device {Mouse, Keyboard, DTrack, General};
    public Device deviceType = Device.Mouse;

    public AnalogRemote analog;
    public ButtonRemote button;
    public TrackerRemote tracker;

    public List<float> analogValues = new List<float>();
        
    public List<bool> buttonValues = new List<bool>();

    public List<Vector3> trackerPosition = new List<Vector3>();

    public List<Quaternion> trackerRotation = new List<Quaternion>();

    public void Start()
    {
        Debug.Log(deviceName + " Client");

        analog = new AnalogRemote(deviceName + '@' + deviceIP);
        analog.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(this.AnalogChanged);
        button = new ButtonRemote(deviceName + '@' + deviceIP, buttonValues.Capacity);
        button.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(this.ButtonChanged);
        tracker = new TrackerRemote(deviceName + '@' + deviceIP);
        tracker.TrackerChanged += new TrackerRemote.TrackerChangeEventHandler(this.TrackerChanged);
    }

    public void Update()
    {
        analog.Mainloop();
        button.Mainloop();
        tracker.Mainloop();
    }

    /* Analog event handler */
    private void AnalogChanged(AnalogEvent e)
    {
        for (int i = 0; i < analogValues.Capacity; i++)
        {
            if(i < analog.LastEvent.getNumChannels())
                analogValues[i] = (float)analog.LastEvent.Channel(i);
        }
    }

    /* Button event handler */
    private void ButtonChanged(ButtonEvent e)
    {
        for (int i = 0; i < buttonValues.Capacity; i++)
        {
            if (i < button.GetNumButtons())
                buttonValues[i] = button.GetButtonState(i);
        }
    }

    /* Tracker event handler */
    private void TrackerChanged(TrackerEvent e)
    {
        for (int i = 0; i < trackerPosition.Capacity; i++)
        {
            trackerPosition[i] = Utils.Convert(tracker.Sensors[i].Position);
        }

        for (int i = 0; i < trackerRotation.Capacity; i++)
        {
            trackerRotation[i] = Utils.Convert(tracker.Sensors[i].Orientation);
        }
    }
}