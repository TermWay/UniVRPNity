using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Vrpn keyboard client for Unity to translate object. 
/// </summary>
public class RotateKeyboard : MonoBehaviour
{
    public RotateAction Actions;
    public ButtonRemoteMB Button;

    public void Start()
    {
        if (Button == null)
            Button = (ButtonRemoteMB)GameObject.FindObjectOfType(typeof(ButtonRemoteMB));
    }

    public void Reset()
    {
        Actions.Transform = this.transform;
        Actions.sensibility = new Vector3(25F, 25F, 25F);
    }

    public void Update()
    {
        Actions.YawLeft = Button.GetButtonState(Keyboard.FrenchKeys.KC_Q);
        Actions.YawRight = Button.GetButtonState(Keyboard.FrenchKeys.KC_D);
        Actions.RollLeft = Button.GetButtonState(Keyboard.FrenchKeys.KC_A);
        Actions.RollRight = Button.GetButtonState(Keyboard.FrenchKeys.KC_E);
        Actions.PitchUp = Button.GetButtonState(Keyboard.FrenchKeys.KC_Z);
        Actions.PitchBottom = Button.GetButtonState(Keyboard.FrenchKeys.KC_S);
    }
}