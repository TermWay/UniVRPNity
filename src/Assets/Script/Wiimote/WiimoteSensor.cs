using UnityEngine;
using UniVRPNity;
using UniVRPNity.Device;
using System.Collections;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Select action menu with the Wii sensor bar.
/// </summary>
public class WiimoteSensor : MonoBehaviour
{
    /// <summary> Wiimote analog. For sensor bar.</summary>
	public AnalogRemoteMB analog;

    /// <summary> Wiimote button.</summary>
    public ButtonRemoteMB button;

    /// <summary>
    /// Is there a visible pointer.
    /// </summary>
    public bool Pointer = true;

    /// <summary>
    /// Computed sensor bar position on screen
    /// </summary>
    protected Vector2 sensor;

    float firstSpotX, firstSpotY, SecondSpotX, SecondSpotY;

     public void OnGUI()
    {
        //Display a sight of the sensor bar.
        if (Pointer && sensor != Vector2.zero)
            GUI.Box(new Rect(sensor.x, sensor.y, 10 , 10), "+");
    }

    public void Update()
	{
        this.updateSensorCoordinate();
	}

    ///<summary>Make an average of the two spots.</summary>
    protected void updateSensorCoordinate()
    {
        sensor = new Vector2();
        float fsX = (float)analog.Channel((int)Wiimote.Analog.FirstSensorSpotX);
        float fsY = (float)analog.Channel((int)Wiimote.Analog.FirstSensorSpotY);
        float ssX = (float)analog.Channel((int)Wiimote.Analog.SecondSensorSpotX);
        float ssY = (float)analog.Channel((int)Wiimote.Analog.SecondSensorSpotY);

        sensor.x = firstSpotX + SecondSpotX;
        sensor.y = firstSpotY + SecondSpotY;

        //ratio with screen size.
        sensor.x = (sensor.x / (1023 * 2)) * Screen.width;
        sensor.y = (sensor.y /  (767 * 2)) * Screen.height;

        if(fsX != -1)
            firstSpotX = fsX;

        if(fsY != -1)
            firstSpotY = fsY;

        if(ssX != -1)
            SecondSpotX = ssX;

        if(ssY != -1)
            SecondSpotY = ssY;
    }
}
