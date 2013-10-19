using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UniVRPNity;
using UniVRPNity.Device;

public class ButtonRemoteMB : RemoteMB
{
    public delegate void ButtonChangeEventHandler(ButtonEvent e);
    public ButtonChangeEventHandler ButtonChanged;

    public ButtonRemote ButtonRemote;

    public Layout Layout = new Layout();

    public void Start()
    {
        this.create();
    }

    protected override void create()
    {
        remote = ButtonRemote = new ButtonRemote(Name + "@" + VRPNAddressServer, 
            ButtonRemote.DefaultButtonNumber,
            UniVRPNityAddressServer,
            UniVRPNityPortServer);
        ButtonRemote.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(ButtonChangedMiddle);
    }

    protected override void destroy()
    {
        ButtonRemote.ButtonChanged -= new ButtonRemote.ButtonChangeEventHandler(ButtonChangedMiddle);
        remote = ButtonRemote = null;

    }

    public int GetNumButtons()
    {
        return ButtonRemote.GetNumButtons();
    }

    public bool GetButtonState(int index)
    {
        return ButtonRemote.GetButtonState(index);
    }

    public bool GetButtonState(Enum index)
    {
        return this.GetButtonState((int) (object)index);
    }

    private void ButtonChangedMiddle(ButtonEvent e)
    {
        if (ButtonChanged != null)
            ButtonChanged(e);
    }
}
