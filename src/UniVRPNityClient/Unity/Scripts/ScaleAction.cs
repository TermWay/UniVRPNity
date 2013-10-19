using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class ScaleAction : BaseTransformAction
    {
        public bool Enlarge, Contract, Raise, Flatten, Elongate, Shorten;

        public void Awake()
        {
            if (Transform == null)
                this.Transform = transform;
        }

        public void Update()
        {
            if (Enlarge)
                Transform.localScale += new Vector3(sensibility.x, 0, 0);
            if (Contract)
                Transform.localScale -= new Vector3(sensibility.x, 0, 0);
            if (Raise)
                Transform.localScale += new Vector3(0, sensibility.y, 0);
            if (Flatten)
                Transform.localScale -= new Vector3(0, sensibility.y, 0);
            if (Elongate)
                Transform.localScale += new Vector3(0, 0, sensibility.z);
            if (Shorten)
                Transform.localScale -= new Vector3(0, 0, sensibility.z);
        }
    }
}


