using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;


/// <summary>
/// Bubble mouse rotation.
/// </summary>
public class RotateMouse : MouseVRPN
{
    public RotateAction Actions;

    public void Reset()
    {
        Actions.Transform = this.transform;
    }

    public override void Update()
    {
        base.Update();

        Actions.sensibility = new Vector3(
            (float)(Math.Abs(diff.y) * multiplier.x),
            (float)(Math.Abs(diff.x) * multiplier.y),
            0);
   
        Actions.YawLeft = diff.x < 0;
        Actions.YawRight = diff.x > 0;
        Actions.PitchUp = diff.y > 0;
        Actions.PitchBottom = diff.y < 0;
    }
}