using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Runtime.Serialization;

using System.Net;
namespace UniVRPNity
{

    public abstract class BaseRemote : MonoBehaviour
    {
        Remote remote;

        public string Name = "Mouse0";

        public IPAddress serverAddress = IPAddress.Loopback;

        public UniVRPNity.Type Type = UniVRPNity.Type.Button;

        public BaseRemote(UniVRPNity.Type type)
        {
            this.Type = type;
        }

        public virtual void Start()
        {
           // remote = new RemoteType(name);
            remote = RemoteFactory.CreateRemote(this, Name + "@" + serverAddress.ToString());
        }

        public virtual void Update()
        {
            remote.Mainloop();
        }

        public virtual void AnalogChanged(UniVRPNity.AnalogEvent e){}
        public virtual void ButtonChanged(UniVRPNity.ButtonEvent e){}
        public virtual void TrackerChanged(UniVRPNity.TrackerEvent e){}
    }

    public class RemoteFactory
    {
        public static Remote CreateRemote(BaseRemote sender, string name)
        {
            Remote remote = null;
            switch (sender.Type)
            {
                case UniVRPNity.Type.Analog:
                    AnalogRemote analog = new AnalogRemote(name);
                    analog.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(sender.AnalogChanged);
                    remote = analog;
                    break;

                case UniVRPNity.Type.Button:
                    ButtonRemote button = new ButtonRemote(name);
                    button.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(sender.ButtonChanged);
                    remote = button;
                    break;

                case UniVRPNity.Type.Tracker:
                    TrackerRemote tracker = new TrackerRemote(name);
                    tracker.TrackerChanged += new TrackerRemote.TrackerChangeEventHandler(sender.TrackerChanged);
                    remote = tracker;
                    break;
            }
            return remote;
        }
    }
}
