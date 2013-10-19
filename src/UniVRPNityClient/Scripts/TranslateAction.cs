using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using UnityEngine;

namespace UniVRPNity
{
    [Serializable]
    public class TranslateAction : BaseTransformAction<Translate>
    {
        public override void Update()
        {
            if (actions[(int)Translate.Forward])
                transform.Translate(0, 0, sensibility.z * Time.deltaTime, Space.Self);
            if (actions[(int)Translate.Backward])
                transform.Translate(0, 0, -sensibility.z * Time.deltaTime, Space.Self);
            if (actions[(int)Translate.Left])
                transform.Translate(-sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (actions[(int)Translate.Right])
                transform.Translate(sensibility.x * Time.deltaTime, 0, 0, Space.Self);
            if (actions[(int)Translate.Up])
                transform.Translate(0, sensibility.y * Time.deltaTime, 0, Space.Self);
            if (actions[(int)Translate.Bottom])
                transform.Translate(0, -sensibility.y * Time.deltaTime, 0, Space.Self);
        }
    }
}


