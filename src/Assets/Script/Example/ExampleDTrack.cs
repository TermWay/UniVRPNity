using System.Text;
using UnityEngine;
using UniVRPNity;

public class ExampleDTrack : MonoBehaviour
{
    public string deviceName = "DTrack";
    public string deviceIP = "localhost";

    public TrackerRemote tracker;

    public Transform transformHandled;

    public int sensor = 0;

    public Vector3 position = new Vector3();
    public Quaternion rotation = new Quaternion();

    private Vector3 positionOffset;
    private Quaternion rotationOffset;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    public void Start()
    {
        tracker = new TrackerRemote(deviceName + '@' + deviceIP);
        tracker.TrackerChanged += new TrackerRemote.TrackerChangeEventHandler(this.TrackerChanged);
    }

    public void Reset()
    {
        transformHandled = transform;
    }

    public void Update()
    {
        tracker.Mainloop();

        //if (transformHandled != null)
        //{
        //    positionOffset = transformHandled.position - lastPosition;
        //    //FIXME Wrong calculation
        //    rotationOffset = new Quaternion(transformHandled.rotation.x - lastRotation.x, transformHandled.rotation.y - lastRotation.y, transformHandled.rotation.z - lastRotation.z, transformHandled.rotation.w - lastRotation.w);

        //    transformHandled.localPosition = position + positionOffset;
        //    //FIXME Wrong calculation
        //    transformHandled.localRotation = new Quaternion(rotation.x + rotationOffset.x, rotation.y + rotationOffset.y, rotation.z + rotationOffset.z, rotation.w + rotationOffset.w);

        //    lastPosition = position;
        //    lastRotation = rotation;
        //}
        transformHandled.localPosition = position;
        transformHandled.localRotation = rotation;
    }

    /* Analog event handler */
    private void TrackerChanged(TrackerEvent e)
    {
        position = Utils.Convert(tracker.Sensors[sensor].Position);
        rotation = Utils.Convert(tracker.Sensors[sensor].Orientation);
    }
}