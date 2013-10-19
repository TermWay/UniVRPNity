using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class ScaleAction : BaseTransformAction<Scale>
    {
        public override void Update()
        {
            if (actions[(int)Scale.Enlarge])
                transform.localScale += new Vector3(sensibility.x, 0, 0);
            if (actions[(int)Scale.Contract])
                transform.localScale -= new Vector3(sensibility.x, 0, 0);
            if (actions[(int)Scale.Raise])
                transform.localScale += new Vector3(0, sensibility.y, 0);
            if (actions[(int)Scale.Flatten])
                transform.localScale -= new Vector3(0, sensibility.y, 0);
            if (actions[(int)Scale.Elongate])
                transform.localScale += new Vector3(0, 0, sensibility.z);
            if (actions[(int)Scale.Shorten])
                transform.localScale -= new Vector3(0, 0, sensibility.z);
        }
    }
}


