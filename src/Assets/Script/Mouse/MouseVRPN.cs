using UnityEngine;

using UniVRPNity;
using UniVRPNity.Device;

/// <summary>
/// A specialized client for mouse using VRPN.
/// 0 ------> 
/// |       x
/// |
/// V  y
/// <typeparam name="Action"></typeparam>
public class MouseVRPN : MonoBehaviour
{
    public AnalogRemoteMB Analog;
    public ButtonRemoteMB Button;
 
    public Vector2 coordinates;
    protected Vector2 lastCoordinates;
    protected Vector2 diff;

    public Vector2 multiplier = new Vector2(30000, 30000);

    public virtual void Start()
    {
        Button.ButtonChanged += new ButtonRemoteMB.ButtonChangeEventHandler(ButtonChanged);
        Analog.AnalogChanged += new AnalogRemoteMB.AnalogChangeEventHandler(AnalogChanged);
    }

    public virtual void Update()
    {
        diff = new Vector2(
            coordinates.x - lastCoordinates.x,
            coordinates.y - lastCoordinates.y);
        
        lastCoordinates.x = coordinates.x;
        lastCoordinates.y = coordinates.y;
    }

    public virtual void ButtonChanged(UniVRPNity.ButtonEvent e)
    {

    }

    public virtual void AnalogChanged(UniVRPNity.AnalogEvent e)
    {
        this.coordinates.x = (float)e.Channel((int)Mouse.Analog.X);
        this.coordinates.y = (float)e.Channel((int)Mouse.Analog.Y);
    }
}