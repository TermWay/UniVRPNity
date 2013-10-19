using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UniVRPNity;


public class TrackerRemoteMB : RemoteMB
{
    public delegate void TrackerChangeEventHandler(TrackerEvent e);
    public TrackerChangeEventHandler TrackerChanged;

    public TrackerRemote TrackerRemote;


    public void Start()
    {
        this.create();
    }

    protected override void create()
    {
        remote = TrackerRemote = new TrackerRemote(Name + "@" + VRPNAddressServer,
            TrackerRemote.DefaultSensorsNumber,
            UniVRPNityAddressServer,
            UniVRPNityPortServer);
        TrackerRemote.TrackerChanged += new TrackerRemote.TrackerChangeEventHandler(TrackerChangedMiddle);
    }

    protected override void destroy()
    {
        TrackerRemote.TrackerChanged -= new TrackerRemote.TrackerChangeEventHandler(TrackerChangedMiddle);
        remote = TrackerRemote = null;
    }


    public List<TrackerEvent> Sensors
    {
        get
        {
            return (TrackerRemote != null)? TrackerRemote.Sensors : null;
        }
    }

    public UnityEngine.Vector3 Position(int sensor)
    {
        return Utils.Convert(Sensors[sensor].Position);
    }

    public UnityEngine.Quaternion Orientation(int sensor)
    {
        return Utils.Convert(Sensors[sensor].Orientation);
    }

    private void TrackerChangedMiddle(TrackerEvent e)
    {
        if (TrackerChanged != null)
            TrackerChanged(e);
    }
}
