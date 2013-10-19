using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class RotateAction : BaseTransformAction<Rotate>
    {
        public override void Update()
        {
            if (actions[(int)Rotate.RollLeft])
                transform.Rotate(0, 0, sensibility.z * Time.deltaTime, Space.World);
            if (actions[(int)Rotate.RollRight])
                transform.Rotate(0, 0, -sensibility.z * Time.deltaTime, Space.World);
            if (actions[(int)Rotate.YawLeft])
                transform.Rotate(0, -sensibility.y * Time.deltaTime, 0, Space.World);
            if (actions[(int)Rotate.YawRight])
                transform.Rotate(0, sensibility.y * Time.deltaTime, 0, Space.World);
            if (actions[(int)Rotate.PitchUp])
                transform.Rotate(-sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (actions[(int)Rotate.PitchBottom])
                transform.Rotate(sensibility.x * Time.deltaTime, 0, 0, Space.Self);
        }
    }
}


