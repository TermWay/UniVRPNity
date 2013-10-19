using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;

/// <summary>
/// Rotate wiimote with nunchuck joystick.
/// </summary>
public class RotateNunchuckAcceleration : WiimoteVRPN
{
    public Vector2 mulitplier = new Vector2(300, 300);

    public RotateAction ActionsTyped;

    public void Reset()
    {
        ActionsTyped.Transform = transform;
    }

    public override void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        base.AnalogChanged(e);

        ActionsTyped.sensibility = new Vector3(
            Math.Abs(accelerationNunchuck.Pitch) * mulitplier.x,
            Math.Abs(accelerationNunchuck.Roll) * mulitplier.y,
            0);

        this.AssignAction();
    }

    private void AssignAction()
    {
        //Can't yaw, so use roll.
        ActionsTyped.YawLeft = accelerationNunchuck.Roll < 0;
        ActionsTyped.YawRight = accelerationNunchuck.Roll > 0;
        ActionsTyped.PitchUp = accelerationNunchuck.Pitch < 0;
        ActionsTyped.PitchBottom = accelerationNunchuck.Pitch > 0;
    }
}