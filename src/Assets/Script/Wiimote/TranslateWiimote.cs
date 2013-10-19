using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Vrpn WiimoteButton client for Unity to translate object. 
/// </summary>
public class TranslateWiimote : WiimoteVRPN
{
    public TranslateAction Actions;
 
    public void Reset()
    {
        Actions.Transform = transform;
    }

    public void Update()
    {
        Actions.Forward = Button.GetButtonState(Wiimote.Button.TOP);
        Actions.Backward = Button.GetButtonState(Wiimote.Button.BOTTOM);
        Actions.Left = Button.GetButtonState(Wiimote.Button.LEFT);
        Actions.Right = Button.GetButtonState(Wiimote.Button.RIGHT);
        Actions.Up = Button.GetButtonState(Wiimote.Button.C);
        Actions.Bottom = Button.GetButtonState(Wiimote.Button.Z);
    }
}