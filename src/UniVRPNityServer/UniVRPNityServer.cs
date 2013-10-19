using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets; 
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml.Serialization;
using Vrpn;


namespace UniVRPNity
{
    /// <summary>
    /// Middle server. Recover information from VrpnNet which take them from vrpn server.
    /// Then wait for C# client to connect and initiate listening.
    /// Need name of the device and it type.
    /// </summary>
    class UniVRPNityServer
    {
        /// <summary>
        /// Max listening client.
        /// </summary>
        const int NUM_CLIENTS = 32;

        /// <summary>
        /// Default time slept between two Update of remotes. In millisecond.
        /// </summary>
        const int DefaultUpdateInterval = 1;

        /// <summary>
        /// Time slept between two Update of remotes. In millisecond.
        /// </summary>
        private int updateInterval;

        /// <summary>
        /// Separator when client send first information (name + type).
        /// </summary>
        const char SEPARATOR = '#';

        /// <summary>
        /// Is the server display information.
        /// </summary>
        private bool verbose = true;

        // Socket stuff used for handling Unity communication
        private Socket middleServer;                 // Socket listen to client request
        private byte[] requestBuffer = new byte[128];   // Receive buffer
        private byte[] sendBuffer = new byte[256];  // Ciphered send buffer

        /// <summary>
        /// All remote objects and its related information.
        /// </summary>
        private VrpnObjects remotes;

        IFormatter formatter; //BinaryFormatter serialize the events
        MemoryStream stream;

        /// <summary>
        /// Is this server have to continue.
        /// </summary>
        private bool toBeContinued = true;

        /// <summary>
        /// Build middle server with a given port.
        /// </summary>
        /// <param name="port"></param>
        public UniVRPNityServer(int port = Network.UniVRPNityServerDefaultPort,
            bool verbose = true,
            int updateInterval = DefaultUpdateInterval)
        {
            Console.WriteLine("==== UniVRPNity middle server ====");
            formatter = new BinaryFormatter();
            remotes = new VrpnObjects();
            this.verbose = verbose;
            this.updateInterval = updateInterval;
            //Create a socket for listening connections
            middleServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Loopback, port);

            try
            {
                middleServer.Bind(ipLocal);
                Console.WriteLine("\r Running on port " + port);
                
                //Run server 
                StartListening();

            }
            catch (SocketException)
            {
                Console.Error.WriteLine("Error : Cannot reach " + ipLocal.ToString() + " address at port " + port);
                Console.Error.WriteLine("An other application is probably using this port.");
                Console.ReadKey();
            }
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
        public void AsyncUpdate()
        {
            while (toBeContinued)
            {
                remotes.SyncUpdate();
                Thread.Sleep(updateInterval); //Avoid CPU eating
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
                case UniVRPNity.Type.Analog:
                    Vrpn.AnalogRemote analog = new Vrpn.AnalogRemote(remote.Name);
                    analog.AnalogChanged += new Vrpn.AnalogChangeEventHandler(this.AnalogChanged);
                    remote.Remote = analog;
                    break;

                case UniVRPNity.Type.Button:
                    Vrpn.ButtonRemote button = new Vrpn.ButtonRemote(remote.Name);
                    button.ButtonChanged += new Vrpn.ButtonChangeEventHandler(this.ButtonChanged);
                    remote.Remote = button;
                    break;

                case UniVRPNity.Type.Tracker:
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
            UniVRPNity.Type type = UniVRPNity.Type.Analog;//Default type.
            if (Enum.IsDefined(typeof(UniVRPNity.Type), receiveType))
            {
                type = (UniVRPNity.Type)Enum.Parse(typeof(UniVRPNity.Type), receiveType, true);
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
            if (remotes.IsPresent(analogSender))
            {
                AnalogEvent analogEvent = new AnalogEvent(this.remotes[analogSender].Name);
                analogEvent.Channels = e.Channels.OfType<double>().ToList();
                analogEvent.Time = e.Time;
                if(verbose)
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
            if (this.remotes.IsPresent(buttonSender))
            {
                ButtonEvent buttonEvent = new ButtonEvent(this.remotes[buttonSender].Name);
                buttonEvent.Button = e.Button;
                buttonEvent.State = e.IsPressed;
                buttonEvent.Time = e.Time;
                if (verbose)
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
            if (remotes.IsPresent(trackerSender))
             {
                TrackerEvent trackerEvent = new TrackerEvent(this.remotes[trackerSender].Name);
                trackerEvent.Sensor = e.Sensor;
                trackerEvent.Position = new UniVRPNityUtils.Vector3((float)e.Position.X,
                    (float)e.Position.Y,
                    (float)e.Position.Z);
                trackerEvent.Orientation = new UniVRPNityUtils.Quaternion((float)e.Orientation.X,
                    (float)e.Orientation.Y,
                    (float)e.Orientation.Z,
                    (float)e.Orientation.W);
                trackerEvent.Time = e.Time;
                if (verbose)
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
                // Begin sending the data to the remote device.
                conveyor.BeginSend(sendBuffer, 0, sendBuffer.Length, 0, new AsyncCallback(SendCallback), conveyor);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Sending error " + se.SocketErrorCode + "(" + se.ErrorCode + ") : " + se.Message + " on " + evt.Device);
                //Quit only if socket is disconnected.
                if (!conveyor.Connected)
                {
                    Console.WriteLine("Client connection error. He maybe quits.");
                    this.RemoveClient(sender);
                }
            } catch (Exception e)  {
                Console.WriteLine(e.ToString());
             }
        }

    private static void SendCallback(IAsyncResult ar)
    {
        try {
            // Retrieve the socket from the state object.
            Socket client = (Socket) ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = client.EndSend(ar);
        } catch (Exception e) {
            Console.WriteLine(e.ToString());
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
            //Port number
            int port =  Network.UniVRPNityServerDefaultPort;
            if (args.Contains("-p") || args.Contains("--port"))
            {
                int index = Math.Max(Array.IndexOf(args, "-p"), Array.IndexOf(args, "--port"));
                if (index + 1 < args.Length)
                    if (!int.TryParse(args[index + 1], out port))
                        Console.Error.WriteLine("Port number " + args[index + 1] + " is not valid");
            }

            //Millisleep interval
            int millisleepInteraval = DefaultUpdateInterval;
            if (args.Contains("-m") || args.Contains("--millisleep"))
            {
                int index = Math.Max(Array.IndexOf(args, "-m"), Array.IndexOf(args, "--millisleep"));
                if (index + 1 < args.Length)
                    if (!int.TryParse(args[index + 1], out millisleepInteraval))
                        Console.Error.WriteLine("Port number " + args[index + 1] + " is not valid");
            }

            //Verbose
            bool verbose = args.Contains("-v") || args.Contains("--verbose");

            //help
            bool help = args.Contains("-h") || args.Contains("--help");

            if(help)
            {
                Console.WriteLine("UniVRPNityServer : Middleware between VRPN and Unity Free. \n" +
                    "-h|--help       : Display help. Do no run the server. \n" +
                    "-v|--verbose    : Display all peripheral event receive from VRPN server and send to the client. \n" +
                    "-p|--port       : Specify the port number of the server. Default port is 8881. \n" +
                    "-m|--millisleep : Time between each internal mainloop in millisecond. Default time is 1ms. \n" +
                    "Example : UniVRPNityServer.exe -v -p 8881 -m 1 -h");
            } else
                new UniVRPNityServer(port, verbose);

            Console.WriteLine("==== Quit ====");
        }
    }
}
