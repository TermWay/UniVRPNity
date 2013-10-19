using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UniVRPNity;

/// <summary>
/// Remote Base class for Unity script. Act as a normal remote but with MonoBehaviour class.
/// </summary>
public abstract class RemoteMB : MonoBehaviour
{
    /// <summary>
    /// Name of the device on the VRPN server.
    /// </summary>
    public string Name;

    /// <summary>
    /// IP of the VRPN server.
    /// </summary>
    public string VRPNAddressServer;

    /// <summary>
    /// IP of the middle VRPN server.
    /// </summary>
    public string UniVRPNityAddressServer;

    /// <summary>
    /// Port of the middle server. Must be 1000 < port < 65635.
    /// </summary>
    public int UniVRPNityPortServer;

    protected UniVRPNity.Remote remote;

    public void Reset()
    {
        Name = "";
        VRPNAddressServer = UniVRPNity.Network.VrpnServerDefaultAddress;
        UniVRPNityAddressServer = UniVRPNity.Network.UniVRPNityServerDefaultAddress;
        UniVRPNityPortServer = UniVRPNity.Network.UniVRPNityServerDefaultPort;
    }

    public void OnEnable()
    {
        this.create();
    }

    public void OnDisable()
    {
        this.destroy();
    }

    public void Update()
    {
        if(remote != null)
            remote.Mainloop();
    }

    public UniVRPNity.Type GetHandlerType()
    {
        return remote.GetHandlerType();
    }

    public override string ToString()
    {
        return remote.ToString();
    }


    public void OnApplicationQuit()
    {
        remote.Quit();
    }


    /// <summary>
    /// Create object and start listening.
    /// </summary>
    protected abstract void create();

    /// <summary>
    /// Destroy all object.
    /// </summary>
    protected abstract void destroy();
}
