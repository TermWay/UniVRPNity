using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class RotateAction : BaseTransformAction
    {
        public bool RollLeft, RollRight, YawLeft, YawRight, PitchUp, PitchBottom;

        public void Awake()
        {
            if (Transform == null)
                this.Transform = transform;
        }

        public void Update()
        {
            if (RollLeft)
                Transform.Rotate(0, 0, sensibility.z * Time.deltaTime, Space.World);
            if (RollRight)
                Transform.Rotate(0, 0, -sensibility.z * Time.deltaTime, Space.World);
            if (YawLeft)
                Transform.Rotate(0, -sensibility.y * Time.deltaTime, 0, Space.World);
            if (YawRight)
                Transform.Rotate(0, sensibility.y * Time.deltaTime, 0, Space.World);
            if (PitchUp)
                Transform.Rotate(-sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (PitchBottom)
                Transform.Rotate(sensibility.x * Time.deltaTime, 0, 0, Space.Self);
        }
    }
}


