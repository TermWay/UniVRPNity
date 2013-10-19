using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// Vrpn keyboard client for Unity to translate object. 
/// </summary>
public class TranslateKeyboard : MonoBehaviour
{
    public TranslateAction Action;
    public ButtonRemoteMB Button;

    public void Start()
    {
        if (Button == null)
            Button = (ButtonRemoteMB)GameObject.FindObjectOfType(typeof(ButtonRemoteMB));
    }

    public void Reset()
    {
        Action = new TranslateAction();
        Action.Transform = this.transform;
    }

    public void Update()
    {
        Action.Forward = Button.GetButtonState(Keyboard.FrenchKeys.KC_HAUT);
        Action.Backward = Button.GetButtonState(Keyboard.FrenchKeys.KC_BAS);
        Action.Left = Button.GetButtonState(Keyboard.FrenchKeys.KC_GAUCHE);
        Action.Right = Button.GetButtonState(Keyboard.FrenchKeys.KC_DROITE);
        Action.Up = Button.GetButtonState(Keyboard.FrenchKeys.KC_PG_SUIV);
        Action.Bottom = Button.GetButtonState(Keyboard.FrenchKeys.KC_PG_PREC);
    }
}