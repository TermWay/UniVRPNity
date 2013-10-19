using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;

/// <summary>
/// Rotate wiimote with nunchuck joystick.
/// </summary>
public class RotateWiimoteAcceleration : WiimoteVRPN
{
    public RotateAction ActionsTyped = new RotateAction();

    public Vector2 multiplier = new Vector2(100, 100);

    public void Reset()
    {
        ActionsTyped.Transform = transform;
    }


    public override void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        base.AnalogChanged(e);

        ActionsTyped.sensibility = new Vector3(
            Math.Abs(accelerationWiimote.Pitch) * multiplier.x,
            Math.Abs(accelerationWiimote.Roll) * multiplier.y,
            0);

        this.AssignAction();
    }

    private void AssignAction()
    {
        //Can't yaw, so use roll.
        ActionsTyped.YawLeft = accelerationWiimote.Roll < 0;
        ActionsTyped.YawRight = accelerationWiimote.Roll > 0;
        ActionsTyped.PitchUp = accelerationWiimote.Pitch < 0;
        ActionsTyped.PitchBottom = accelerationWiimote.Pitch > 0;
    }
}