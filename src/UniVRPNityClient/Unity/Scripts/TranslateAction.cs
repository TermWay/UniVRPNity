using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class TranslateAction : BaseTransformAction
    {
        public bool Forward, Backward, Left, Right, Up, Bottom;

        public void Awake()
        {
            if (Transform == null)
                this.Transform = transform;
        }

        public void Update()
        {
            if (Forward)
                Transform.Translate(0, 0, sensibility.z * Time.deltaTime, Space.Self);
            if (Backward)
                Transform.Translate(0, 0, -sensibility.z * Time.deltaTime, Space.Self);
            if (Left)
                Transform.Translate(-sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (Right)
                Transform.Translate(sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (Up)
                Transform.Translate(0, sensibility.y * Time.deltaTime, 0, Space.Self);
            if (Bottom)
                Transform.Translate(0, -sensibility.y * Time.deltaTime, 0, Space.Self);
        }
    }
}


