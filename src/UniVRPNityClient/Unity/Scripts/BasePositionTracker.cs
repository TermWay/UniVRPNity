using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    public abstract class BasePositionTracker : BaseRemote
    {
        public int Sensor;
        public Vector3 Position;
        public Quaternion Rotation;

        private Vector3 positionOffset;
        private Quaternion rotationOffset;

        private Vector3 lastPosition;
        private Quaternion lastRotation;

        public BasePositionTracker() : base(UniVRPNity.Type.Tracker) { }

        public override void Start()
        {
            base.Start();
            Sensor = 0;
            Position = new Vector3();
            Rotation = new Quaternion();
            positionOffset = new Vector3();
            rotationOffset = new Quaternion();
            lastPosition = new Vector3();
            lastRotation = new Quaternion();
        }

        public override void Update()
        {
            base.Update();

            positionOffset = transform.position - lastPosition;
            rotationOffset = new Quaternion(transform.rotation.x - lastRotation.x, transform.rotation.y - lastRotation.y, transform.rotation.z - lastRotation.z, transform.rotation.w - lastRotation.w);

            transform.position = Position + positionOffset;
            transform.rotation = new Quaternion(Rotation.x + rotationOffset.x, Rotation.y + rotationOffset.y, Rotation.z + rotationOffset.z, Rotation.w + rotationOffset.w);
            
            lastPosition = Position;
            lastRotation = Rotation;
        }

        public override void TrackerChanged(UniVRPNity.TrackerEvent e)
        {
            try
            {
                if (Sensor == e.Sensor)
                {
                    Position = Utils.Convert(e.Position);
                    Rotation = Utils.Convert(e.Orientation);
                }
            }
            catch (InvalidOperationException) { /* Not found */}
        }
    }

}


