using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

public class ScaleKeyboard : MonoBehaviour
{
    public ScaleAction ActionsTyped;
    public ButtonRemoteMB Button;

    public void Start()
    {
        if (Button == null)
            Button = (ButtonRemoteMB)GameObject.FindObjectOfType(typeof(ButtonRemoteMB));
    }

    public void Reset()
    {
        ActionsTyped = new ScaleAction();
        ActionsTyped.Transform = this.transform;
        ActionsTyped.sensibility = new Vector3(0.2F, 0.2F, 0.2F);
    }

    public void Update()
    {
        ActionsTyped.Enlarge = Button.GetButtonState(Keyboard.FrenchKeys.KC_J);
        ActionsTyped.Contract = Button.GetButtonState(Keyboard.FrenchKeys.KC_L);
        ActionsTyped.Raise =  Button.GetButtonState(Keyboard.FrenchKeys.KC_I);
        ActionsTyped.Flatten = Button.GetButtonState(Keyboard.FrenchKeys.KC_K);
        ActionsTyped.Elongate = Button.GetButtonState(Keyboard.FrenchKeys.KC_O);
        ActionsTyped.Shorten = Button.GetButtonState(Keyboard.FrenchKeys.KC_U);
    }
}