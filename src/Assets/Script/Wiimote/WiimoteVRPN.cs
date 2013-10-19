using System;
using System.Collections.Generic;

using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;


[Serializable]
public class Acceleration
{
    public Acceleration(float roll, float pitch, float yaw)
    {
        this.Roll = roll;
        this.Pitch = pitch;
        this.Yaw = yaw;
    }


    /// <summary>
    /// X.
    /// </summary>
    public float Pitch;

    /// <summary>
    /// Y.
    /// </summary>
    public float Yaw;

    /// <summary>
    /// Z.
    /// </summary>
    public float Roll;
}


[Serializable]
public class Joystick
{
    public Joystick(float angle, float force)
    {
        this.Angle = angle; //Radian
        this.Magnitude = force; //[0..1]
    }
    public float Angle;
    public float Magnitude;
}




/// <summary>
/// Vrpn WiimoteButton client for Unity to translate object. 
/// </summary>
public class WiimoteVRPN : MonoBehaviour
{
    public ButtonRemoteMB Button;
    public AnalogRemoteMB Analog;


    /// <summary>
    /// Acceleration of the wiimote. (Roll, Pitch, Yaw)
    /// </summary>
    public Acceleration accelerationWiimote;

    /// <summary>
    /// Acceleration of the nunchuck. (Roll, Pitch, Yaw)
    /// </summary>
    public Acceleration accelerationNunchuck;

    /// <summary>
    /// Offset for acceleration. 
    /// Default increase pitch because hand can't pitch down a lot a wiimote.
    /// </summary>
    public Acceleration offsetWiimote = new Acceleration(0, +0.3F, 0);

    /// <summary>
    /// Offset for acceleration. 
    /// Default increase pitch because hand can't pitch down a lot a nunchuck.
    /// </summary>
    public Acceleration offsetNunchuck = new Acceleration(0, +0.1F, 0);

    public virtual void Start()
    {
        Button.ButtonChanged += new ButtonRemoteMB.ButtonChangeEventHandler(ButtonChanged);
        Analog.AnalogChanged += new AnalogRemoteMB.AnalogChangeEventHandler(AnalogChanged);
    }

    public virtual void ButtonChanged(UniVRPNity.ButtonEvent e)
    {
    }

    public virtual void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        accelerationWiimote = new Acceleration(
            (float)e.Channel((int)Wiimote.Analog.Roll) + offsetWiimote.Roll,
            (float)e.Channel((int)Wiimote.Analog.Pitch) + offsetWiimote.Pitch,
            (float)e.Channel((int)Wiimote.Analog.Yaw) + offsetWiimote.Yaw
            );

        accelerationNunchuck = new Acceleration(
            (float)e.Channel((int)Wiimote.Analog.NunchuckRoll) + offsetNunchuck.Roll,
            (float)e.Channel((int)Wiimote.Analog.NunchuckPitch) + offsetNunchuck.Pitch,
            (float)e.Channel((int)Wiimote.Analog.NunchuckYaw) + offsetNunchuck.Yaw
            );
    }
}