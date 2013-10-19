using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Bubble mouse rotation.
/// </summary>
public class AnimateTimeMouse : MouseVRPN
{
    public AnimateTimeAction ActionsTyped;

    public void Reset()
    {
        ActionsTyped.Animation = this.animation;
    }

    public override void Update()
    {
        base.Update();

        ActionsTyped.Play = Button.GetButtonState((int)Mouse.Button.RIGHT);

        ActionsTyped.Pause = Button.GetButtonState((int)Mouse.Button.MIDDLE);

        ActionsTyped.Stop = Button.GetButtonState((int)Mouse.Button.LEFT) &&
            Button.GetButtonState((int)Mouse.Button.RIGHT);
        
        ActionsTyped.Rewind = diff.x < 0 &&
            Button.GetButtonState((int)Mouse.Button.LEFT);
        
        ActionsTyped.Forward = diff.x > 0 &&
            Button.GetButtonState((int)Mouse.Button.LEFT);
    }
}