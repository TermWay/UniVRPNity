using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UniVRPNity;
using UniVRPNity.Device;

public class AnalogRemoteMB : RemoteMB
{
    public delegate void AnalogChangeEventHandler(AnalogEvent e);
    public AnalogChangeEventHandler AnalogChanged;

    public AnalogRemote AnalogRemote;

    public Layout Layout = new Layout();

  
    public void Start()
    {
        this.create();
    }

    protected override void create()
    {
        remote = AnalogRemote = new AnalogRemote(Name + "@" + VRPNAddressServer,
            UniVRPNityAddressServer,
            UniVRPNityPortServer);
        AnalogRemote.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(AnalogChangedMiddle);
    }

    protected override void destroy()
    {
        AnalogRemote.AnalogChanged -= new AnalogRemote.AnalogChangeEventHandler(AnalogChangedMiddle);
        remote = AnalogRemote = null;
    }

    public double Channel(int c)
    {
        return AnalogRemote.Channel(c);
    }

    public double Channel(Enum c)
    {
        return this.Channel((int) (object) c);
    }

    public AnalogEvent LastEvent
    {
        get
        {
            return AnalogRemote.LastEvent;
        }
    }

    private void AnalogChangedMiddle(AnalogEvent e)
    {
        if (AnalogChanged != null)
            AnalogChanged(e);
    }
}
