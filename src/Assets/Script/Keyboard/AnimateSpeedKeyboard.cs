using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Vrpn keyboard client for Unity to animate object. 
/// </summary>
public class AnimateSpeedKeyboard : MonoBehaviour
{
    public AnimateSpeedAction ActionsTyped;
    public ButtonRemoteMB Button;

    public virtual void Start()
    {
        if (Button == null)
            Button = (ButtonRemoteMB)GameObject.FindObjectOfType(typeof(ButtonRemoteMB));
    }

    public void Reset()
    {
        ActionsTyped.Animation = this.animation;
    }

    public void Update()
    {
        ActionsTyped.animationState = animation[animation.clip.name];
        ActionsTyped.Play = Button.GetButtonState(Keyboard.FrenchKeys.KC_HAUT);
        ActionsTyped.Pause = Button.GetButtonState(Keyboard.FrenchKeys.KC_BAS);
        ActionsTyped.Stop = Button.GetButtonState(Keyboard.FrenchKeys.KC_CTRL_DROITE);
        ActionsTyped.Rewind = Button.GetButtonState(Keyboard.FrenchKeys.KC_GAUCHE);
        ActionsTyped.Forward = Button.GetButtonState(Keyboard.FrenchKeys.KC_DROITE);
    }
}