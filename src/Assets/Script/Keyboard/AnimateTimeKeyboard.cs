using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Vrpn keyboard client for Unity to animate object. 
/// </summary>
public class AnimateTimeKeyboard : MonoBehaviour
{
    public AnimateTimeAction Actions;
    public ButtonRemoteMB Button;

    public void Start()
    {
        if (Button == null)
            Button = (ButtonRemoteMB)GameObject.FindObjectOfType(typeof(ButtonRemoteMB));
    }

    public void Reset()
    {
        Actions.Animation = this.animation;
    }

    public void Update()
    {
        Actions.Play = Button.GetButtonState(Keyboard.FrenchKeys.KC_HAUT);
        Actions.Pause = Button.GetButtonState(Keyboard.FrenchKeys.KC_BAS);
        Actions.Stop = Button.GetButtonState(Keyboard.FrenchKeys.KC_CTRL_DROITE);
        Actions.Rewind = Button.GetButtonState(Keyboard.FrenchKeys.KC_GAUCHE);
        Actions.Forward = Button.GetButtonState(Keyboard.FrenchKeys.KC_DROITE);
    }
}