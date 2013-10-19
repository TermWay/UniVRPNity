using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;


/// <summary>
/// Bubble mouse rotation.
/// </summary>
public class RotateBubbleMouse : MouseVRPN
{
    public RotateAction Actions;

    public double Left = 0.1;
    public double Right = 0.9;
    public double Up = 0.1;
    public double Down = 0.9;

    public void Reset()
    {
        Actions.Transform = this.transform;
        Actions.sensibility = new Vector3(25F, 25F, 25F);
    }

    public override void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        base.AnalogChanged(e);
        
        Actions.YawLeft = this.coordinates.x < Left;
        Actions.YawRight = this.coordinates.x > Right;
        Actions.PitchUp = this.coordinates.y < Up;
        Actions.PitchBottom = this.coordinates.y > Down;
    }
}