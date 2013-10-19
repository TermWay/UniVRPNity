using System;
using System.Text;
using UnityEngine;
using UniVRPNity;

public class ExampleMouse : MonoBehaviour
{
    public string deviceName = "Mouse0";
    public string deviceIP = "localhost";

    public AnalogRemote analog;
    public ButtonRemote button;

    //Apply the script on this transform
    public Transform transformHandled;
        
    public bool mouseLeft = false;
    public bool mouseMiddle = false;
    public bool mouseRight = false;

    public Vector2 coordinates = new Vector2();

    public Vector2 sensibility = new Vector2(30000, 30000);

    public bool bubble = false;
    public Vector2 sensibilityBubble = new Vector2(300, 300);
    public double bubbleLeft = 0.001;
    public double bubbleRight = 0.999;
    public double bubbleUp = 0.001;
    public double bubbleDown = 0.999;

    private Vector2 lastCoordinates = new Vector2(0.5F, 0.5F);

    private Vector2 turn = new Vector2();

    public void Start()
    {
        analog = new AnalogRemote(deviceName + '@' + deviceIP);
        analog.AnalogChanged += new AnalogRemote.AnalogChangeEventHandler(this.AnalogChanged);
        button = new ButtonRemote(deviceName + '@' + deviceIP, 3);
        button.ButtonChanged += new ButtonRemote.ButtonChangeEventHandler(this.ButtonChanged);
    }

    public void Reset()
    {
        transformHandled = transform;
    }

    public void Update()
    {
        analog.Mainloop();
        button.Mainloop();
            
        if (coordinates.x < bubbleLeft && bubble)
            turn.x = (float)(coordinates.x - 0.5) * sensibilityBubble.x;
        else if (coordinates.x > bubbleRight && bubble)
            turn.x = (float)(coordinates.x - 0.5) * sensibilityBubble.x;
        else
            turn.x = (float)(coordinates.x - lastCoordinates.x) * sensibility.x;

        if (coordinates.y < bubbleUp && bubble)
            turn.y = (float)(coordinates.y - 0.5) * sensibilityBubble.y;
        else if (coordinates.y > bubbleDown && bubble)
            turn.y = (float)(coordinates.y - 0.5) * sensibilityBubble.y;
        else
            turn.y = (float)(coordinates.y - lastCoordinates.y) * sensibility.y;

        if (transformHandled != null)
        {
            transformHandled.Rotate(0, turn.x * Time.deltaTime, 0, Space.World);
            transformHandled.Rotate(turn.y * Time.deltaTime, 0, 0, Space.Self);
        }

        lastCoordinates.x = coordinates.x;
        lastCoordinates.y = coordinates.y;
    }

    /* Analog event handler */
    private void AnalogChanged(AnalogEvent e)
    {
        mouseLeft = button.GetButtonState(0);
        mouseMiddle = button.GetButtonState(1);
        mouseRight = button.GetButtonState(2);

        coordinates.x = (float)analog.LastEvent.Channel(0);
        coordinates.y = (float)analog.LastEvent.Channel(1);
    }

    private void ButtonChanged(ButtonEvent e)
    {
        
    }
}