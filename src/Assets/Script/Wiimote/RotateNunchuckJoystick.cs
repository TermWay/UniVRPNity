using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Rotate wiimote with nunchuck joystick.
/// </summary>
public class RotateNunchuckJoystick : WiimoteVRPN
{
    public RotateAction ActionsTyped;

    /// <summary>
    /// Angle and force.
    /// </summary>
    public Joystick joystick;

    public Vector2 joystickPosition;

    /// <summary>
    /// Do not consider if force is under this delta.
    /// </summary>
    public float delta = 0.05F;

    public Vector2 multiplier = new Vector2(100, 100);

    public void Reset()
    {
        ActionsTyped.Transform = transform;
    }

    public override void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        base.AnalogChanged(e);
        joystick = new Joystick((float)e.Channel((int)Wiimote.Analog.JoystickAngle),
                               (float)e.Channel((int)Wiimote.Analog.JoystickMagnitude));
        if (joystick.Magnitude < delta)
            joystick.Magnitude = 0;

        joystickPosition = new Vector2(
           (float)Math.Sin(Math.PI * joystick.Angle / 180),
           (float)Math.Cos(Math.PI * joystick.Angle / 180));

        ActionsTyped.sensibility = new Vector3(
            joystick.Magnitude * Math.Abs(joystickPosition.y) * multiplier.x,
            joystick.Magnitude * Math.Abs(joystickPosition.x) * multiplier.y,
            0);

        this.AssignAction();
    }

    private void AssignAction()
    {
        ActionsTyped.YawLeft = joystickPosition.x < 0;
        ActionsTyped.YawRight = joystickPosition.x > 0;
        ActionsTyped.PitchUp = joystickPosition.y > 0;
        ActionsTyped.PitchBottom = joystickPosition.y < 0;
    }
}