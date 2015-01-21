# How to test?

* Download the binaries
* Run vrpnserver
* Run UniVRPNityServer
* Open the sample unity scene
* Start the scene

Actions:
* Arrow for moving the cube
* Mouse to moving the 3D text

# How to integrate into your scene?

* Copy paste UniVRPNityCommon.dll & UniVRPNityClient.dll Into your Assets folder
* Merge the UniVRPNity Assets folder with your Assets folder

# How to use your own device?

* Create remote(s) device(s) (Analog, Button, Tracker)
* Specify the name of the device (see your vrpn config file)
* Create a MonoBehaviour script with remote(s) device(s) as public attribute
* Link the remote device with unity editor
* Recover informations from the remove device into Update method
    
## Analog informations

* `public double Channel(int c)`
* `public AnalogEvent LastEvent`

## Button informations
* `public int GetNumButtons()`
* `public bool GetButtonState(int index)`

## Tracker informations
* `public List<TrackerEvent> Sensors`

### TrackerEvent

* `public Quaternion Orientation;`
* `public Vector3 Position;`
* `public int Sensor;`

# How to compile?

* Add your UnityEngine.dll & UnityEditor when it's needed
* Add VrpnNet.dll to the UniVRPNityServer

Note:
* Use .NET v3.5
* Use Mono 2.0 in Unity settings (Not Subset)

* For [VRPN](http://www.cs.unc.edu/Research/vrpn/vrpn_standard_stuff.html)
* For [VrpnNet](http://wwwx.cs.unc.edu/~chrisv/vrpnnet)

# For more detail

* Read the [documentation](https://github.com/TermWay/UniVRPNity/blob/master/src/Documentation/Documentation.pdf) 
