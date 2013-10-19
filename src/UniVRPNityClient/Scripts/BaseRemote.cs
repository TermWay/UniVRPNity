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

        public EventHandler.Type Type = EventHandler.Type.Button;

        public BaseRemote(EventHandler.Type type)
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

        public virtual void AnalogChanged(EventHandler.AnalogEvent e){}
        public virtual void ButtonChanged(EventHandler.ButtonEvent e){}
        public virtual void TrackerChanged(EventHandler.TrackerEvent e){}
    }

    public class RemoteFactory
    {
        public static Remote CreateRemote(BaseRemote sender, string name)
        {
            Remote remote = null;
            switch (sender.Type)
            {
                case EventHandler.Type.Analog:
                    AnalogRemote analog = new AnalogRemote(name);
                    analog.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(sender.AnalogChanged);
                    remote = analog;
                    break;

                case EventHandler.Type.Button:
                    ButtonRemote button = new ButtonRemote(name);
                    button.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(sender.ButtonChanged);
                    remote = button;
                    break;

                case EventHandler.Type.Tracker:
                    TrackerRemote tracker = new TrackerRemote(name);
                    tracker.TrackerChanged += new TrackerRemote.TrackerChangeEventHandler(sender.TrackerChanged);
                    remote = tracker;
                    break;
            }
            return remote;
        }
    }
}
