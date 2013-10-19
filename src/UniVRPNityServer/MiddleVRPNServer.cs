//#define __VERBOSE__

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;   // For communicating with Unity3D
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Vrpn;
using EventHandler;



namespace UniVRPNity
{
    /// <summary>
    /// Store related information about remote :
    /// <ul>
    /// <li>Name of the remote device.</li>
    /// <li>Type of device (button, analog, tracker).</li>
    /// <li>Socket to communicate with client.</li>
    /// </ul>
    /// </summary>
    class VrpnObjectInfo
    {
        /// <summary>
        /// Remote device of VrpnNet.
        /// </summary>
        public IVrpnObject Remote;

        /// <summary>
        /// Name of the device. Do not contain address.
        /// </summary>
        public string Name;

        /// <summary>
        /// Type of the device (button, analog, tracker).
        /// </summary>
        public EventHandler.Type Type;

        /// <summary>
        /// Socket from the client.
        /// </summary>
        public Socket Socket;

        /// <summary>
        /// Build a object without the socket.
        /// </summary>
        /// <param name="name">See <see cref="VrpnObjects.Name"/>.</param>
        /// <param name="type">See <see cref="VrpnObjects.Type"/>.</param>
        public VrpnObjectInfo(string name, EventHandler.Type type)
        {
            this.Name = name;
            this.Type = type;
        }
    }

    class VrpnObjects
    {
        /// <summary>
        /// Synchronisation object.
        /// </summary>
        private readonly object sync = new object();

        /// <summary>
        /// Monitoring the state of the remotes iterator.
        /// </summary>
        private bool valid = true;

        /// <summary>
        /// Contains all remotes informations.
        /// </summary>
        private HashSet<VrpnObjectInfo> remotes;

        /// <summary>
        /// Build an empty remotes list.
        /// </summary>
        public VrpnObjects()
        {
            remotes = new HashSet<VrpnObjectInfo>();
        }

        /// <summary>
        /// Invalidate iterator state. <see cref="Valid"/>.
        /// </summary>
        public void invalidate()
        {
            this.valid = false;
        }

        /// <summary>
        /// Validate iterator state. <see cref="Valid"/>.
        /// </summary>
        public void validate()
        {
            this.valid = true;
        }

        /// <summary>
        /// Recover iterator state.
        /// </summary>
        public bool Valid
        {
            get 
            {
                return this.valid;
            }
        }

        /// <summary>
        /// Add remote object on the list. Synchronised.
        /// </summary>
        /// <param name="remote">Remote object to add.</param>
        public void SyncAdd(VrpnObjectInfo remote)
        {
            lock (sync)
            {
                remotes.Add(remote);
            }
            this.invalidate();
        }

        /// <summary>
        /// Remove a remote object on the list. Synchronised.
        /// </summary>
        /// <param name="remote">Remote object to remove.</param>s
        public void SyncRemove(VrpnObjectInfo remote)
        {
            lock (sync)
            {
                remotes.Remove(remote);
            }
            this.invalidate();
         }

        /// <summary>
        /// Call update of all remote object.
        /// </summary>
        public void SyncUpdate()
        {
            lock (sync)
            {
                foreach (VrpnObjectInfo peripheral in remotes)
                {
                    peripheral.Remote.Update();
                    if (!this.Valid)
                        break;
                }
                this.validate();
            }
        }

        /// <summary>
        /// Search a remote device.
        /// </summary>
        /// <param name="vrpnObject">Searched object.</param>
        /// <returns>True if exist, false else.</returns>
        public bool isPresent(IVrpnObject vrpnObject)
        {
            return this[vrpnObject] != null;
        }

        /// <summary>
        /// Find a remote from it name. Iterate on all object : O(n).
        /// </summary>
        /// <param name="name">Name of the remote.</param>
        /// <returns>Found object, null else.</returns>
        public VrpnObjectInfo this[string name]
        {
            get 
            {
                foreach (VrpnObjectInfo r in remotes)
                    if (r.Name == name)
                        return r;
                return null;
            }
        }

        /// <summary>
        /// Find a remote from it VrpnNet remote object. Iterate on all object : O(n).
        /// </summary>
        /// <param name="name">VrpnNet remote object.</param>
        /// <returns>Found object, null else.</returns>
        public VrpnObjectInfo this[IVrpnObject remote]
        {
            get
            {
                foreach (VrpnObjectInfo r in remotes)
                    if (r.Remote == remote)
                        return r;
                return null;
            }
        }
               
    }

    /// <summary>
    /// Middle server. Recover information from VrpnNet which take them from vrpn server.
    /// Then wait for C# client to connect and initiate listening.
    /// Need name of the device and it type.
    /// </summary>
    class MiddleVRPNServer
    {
        /// <summary>
        /// Max listening client.
        /// </summary>
        const int NUM_CLIENTS = 10;

        /// <summary>
        /// Default port.
        /// </summary>
        const int DEFAULT_PORT = 8881;

        /// <summary>
        /// Time slept between two Update of remotes. In millisecond.
        /// </summary>
        const int UPDATE_INTERVAL = 1;
 
        /// <summary>
        /// Separator when client send first information (name + type).
        /// </summary>
        const char SEPARATOR = '#';

        // Socket stuff used for handling Unity communication
        private Socket middleServer;                 // Socket listen to client request
        private byte[] requestBuffer = new byte[128];   // Receive buffer
        private byte[] sendBuffer = new byte[256];  // Ciphered send buffer

        /// <summary>
        /// All remote objects and its related information.
        /// </summary>
        private VrpnObjects remotes;

        BinaryFormatter formatter; //BinaryFormatter serialize the events
        MemoryStream stream;

        /// <summary>
        /// Is this server have to continue.
        /// </summary>
        private bool toBeContinued = true;

        /// <summary>
        /// Build middle server with a given port.
        /// </summary>
        /// <param name="port"></param>
        public MiddleVRPNServer(int port = DEFAULT_PORT)
        {
            Console.WriteLine("==== UniVRPNity middle server ====");
            Console.WriteLine("\r Running on port " + port);
            remotes = new VrpnObjects();
            //Create a socket for listening connections
            middleServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
            middleServer.Bind(ipLocal);
            formatter = new BinaryFormatter();

            //Run server 
            StartListening();
        }

        /// <summary>
        /// Start middle server.
        /// </summary>
        private void StartListening()
        {
            //Listening event with a dedicated thread. Asynchrone.
            Thread updateThread = new Thread(this.AsyncUpdate);
            updateThread.Start();

            middleServer.Listen(NUM_CLIENTS);
            while (toBeContinued)
            {
                Socket listen = middleServer.Accept();
                int sizeRequest = listen.Receive(requestBuffer);
                VrpnObjectInfo remote = this.ReceiveRequestClient(requestBuffer, sizeRequest);
                remote.Socket = listen;
                remotes.SyncAdd(remote);
            }
        }

        /// <summary>
        /// Update reception of event.
        /// </summary>
        public void AsyncUpdate(){         
            while (toBeContinued)
            {
                remotes.SyncUpdate();
                Thread.Sleep(UPDATE_INTERVAL); //Avoid CPU eating
            }
        }

        /// <summary>
        /// Create VrpnNet remote object associate to the client request. Init listening.
        /// </summary>
        /// <param name="request">Raw client request (from socket).</param>
        /// <param name="sizeRequest">Size of the request (can't use length of array).</param>
        /// <returns>A remote object with needed information.</returns>
        public VrpnObjectInfo ReceiveRequestClient(byte[] request, int sizeRequest)
        {
            string peripheral = System.Text.Encoding.ASCII.GetString(requestBuffer, 0, sizeRequest);
            VrpnObjectInfo remote = this.ParseRequestClient(peripheral);

            switch (remote.Type)
            {
                case EventHandler.Type.Analog:
                    Vrpn.AnalogRemote analog = new Vrpn.AnalogRemote(remote.Name);
                    analog.AnalogChanged += new Vrpn.AnalogChangeEventHandler(this.AnalogChanged);
                    remote.Remote = analog;
                    break;

                case EventHandler.Type.Button:
                    Vrpn.ButtonRemote button = new Vrpn.ButtonRemote(remote.Name);
                    button.ButtonChanged += new Vrpn.ButtonChangeEventHandler(this.ButtonChanged);
                    remote.Remote = button;
                    break;

                case EventHandler.Type.Tracker:
                    Vrpn.TrackerRemote tracker = new Vrpn.TrackerRemote(remote.Name);
                    tracker.PositionChanged += new Vrpn.TrackerChangeEventHandler(this.TrackerChanged);
                    remote.Remote = tracker;
                    break;
                
                default:
                    Console.WriteLine("Unknown peripheral type: " + remote.Type + " (Should be never call)");
                    remote = null;
                    break;
            }
            remote.Remote.MuteWarnings = true;
            return remote;
        }


        /// <summary>
        /// Parse the client request with it name and type.
        /// </summary>
        /// <param name="request">Contains device name to listen and it type (button, analog, tracker).</param>
        /// <returns>Useable remote object.</returns>
        private VrpnObjectInfo ParseRequestClient(string request)
        {
            Console.WriteLine("Server - receive connection: " + request);
            string[] nameTypeperipheral = request.Split(SEPARATOR);
            if (nameTypeperipheral.Length != 2)
            {
                throw new ArgumentException("Bad number of argument of the client to the server! Expected:2, given: " + nameTypeperipheral.Length);    
            }
            string name = request.Split(SEPARATOR)[0];
            string receiveType = request.Split(SEPARATOR)[1];
            EventHandler.Type type = EventHandler.Type.Analog;//Default type.
            if (Enum.IsDefined(typeof(EventHandler.Type), receiveType))
            {
                type = (EventHandler.Type)Enum.Parse(typeof(EventHandler.Type), receiveType, true);
                Console.WriteLine("peripheral " + name + " of type " + receiveType + " connected.");
            }
            else
                Console.WriteLine("Type of peripheral not defined: >" + receiveType + "<");
            return new VrpnObjectInfo(name, type);
        }

        

        /* Analog event handler */
        private void AnalogChanged(object sender, AnalogChangeEventArgs e)
        {
            Vrpn.AnalogRemote analogSender = (Vrpn.AnalogRemote)sender;
            //sender.GetType() == Vrpn.AnalogRemote
            if (remotes.isPresent(analogSender))
            {
                AnalogEvent analogEvent = new AnalogEvent(this.remotes[analogSender].Name);
                analogEvent.Channels = e.Channels.OfType<double>().ToList();
                analogEvent.Time = e.Time;
                Console.WriteLine("Analog: " + analogEvent);
                this.SendEvent(analogSender, analogEvent);
            }
            else
            {
                analogSender.AnalogChanged -= new Vrpn.AnalogChangeEventHandler(this.AnalogChanged);
            }
        }

        /* Button event handler */
        private void ButtonChanged(object sender, ButtonChangeEventArgs e)
        {
            Vrpn.ButtonRemote buttonSender = (Vrpn.ButtonRemote) sender;
            if (this.remotes.isPresent(buttonSender))
            {
                ButtonEvent buttonEvent = new ButtonEvent(this.remotes[buttonSender].Name);
                buttonEvent.Button = e.Button;
                buttonEvent.State = e.IsPressed;
                buttonEvent.Time = e.Time;
                Console.WriteLine("Button: " + buttonEvent);
                this.SendEvent(buttonSender, buttonEvent);
            }
            else
            {
                buttonSender.ButtonChanged -= new Vrpn.ButtonChangeEventHandler(this.ButtonChanged);
            }
        }

        /* Analog event handler */
        private void TrackerChanged(object sender, TrackerChangeEventArgs e)
        {
            Vrpn.TrackerRemote trackerSender = (Vrpn.TrackerRemote) sender;
            if (remotes.isPresent(trackerSender))
             {
                TrackerEvent trackerEvent = new TrackerEvent(this.remotes[trackerSender].Name);
                trackerEvent.Sensor = e.Sensor;
                trackerEvent.Position = new EventHandlerUtils.Vector3((float)e.Position.X,
                    (float)e.Position.Y,
                    (float)e.Position.Z);
                trackerEvent.Orientation = new EventHandlerUtils.Quaternion((float)e.Orientation.X,
                    (float)e.Orientation.Y,
                    (float)e.Orientation.Z,
                    (float)e.Orientation.W);
                trackerEvent.Time = e.Time;
                Console.WriteLine("Tracker: " + trackerEvent);
                this.SendEvent(trackerSender, trackerEvent);
             }
             else
             {
                 trackerSender.PositionChanged -= new Vrpn.TrackerChangeEventHandler(this.TrackerChanged);
             }
        }

        /// <summary>
        /// Send event to the associate client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        private void SendEvent(IVrpnObject sender, Event evt)
        {
            stream = new MemoryStream();
            formatter.Serialize(stream, evt);
            sendBuffer = stream.GetBuffer();
            Socket conveyor = this.remotes[sender].Socket;
            try
            {
                conveyor.Send(sendBuffer);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client connection error. He maybe quits.");
                this.RemoveClient(sender);
            }
        }

        /// <summary>
        /// Remove a client when it shutdown the connection.
        /// </summary>
        /// <param name="socket">Client socket.</param>
        /// <param name="sender">Remote object associate to the client.</param>
        void RemoveClient(IVrpnObject sender)
        {
            //sender.GetConnection().Dispose(); // Don't even think about it. Crash at 100%.
            VrpnObjectInfo remote = this.remotes[sender];
            Socket socket = remote.Socket;
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            this.remotes.SyncRemove(remote);
            Console.WriteLine("Close client connection.");
        }
      
        static void Main(string[] args)
        {
            int port = ((args.Length == 1)? int.Parse(args[0]) : 8881);
            MiddleVRPNServer theMiddleVRPNServer = new MiddleVRPNServer(port);
        }
    }
}
