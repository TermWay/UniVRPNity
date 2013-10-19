using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace UniVRPNity
{
    public class StateObject 
    {
        // Client  socket.
        public Socket WorkSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 2048;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
    
    }

    public class MiddleVRPNClient
    {
        public const char SEPARATOR = '#';

        /// <summary>
        /// Last AnalogEvent receive by the client.
        /// </summary>
        public AnalogEvent analogEvent = new AnalogEvent();
        /// <summary>
        /// Last ButtonEvent receive by the client.
        /// </summary>
        public ButtonEvent buttonEvent = new ButtonEvent();
        /// <summary>
        /// Last TrackerEvent receive by the client.
        /// </summary>
        public TrackerEvent trackerEvent = new TrackerEvent();

        /// <summary>
        /// Stock the events when they come before processing.
        /// </summary>
        private BufferEvent bufferEvent = new BufferEvent();

        /// <summary>
        /// Used to deserialize.
        /// </summary>
        BinaryFormatter formatter;
        /// <summary>
        /// Used to deserialize.
        /// </summary>
        MemoryStream memStream;

        public Remote remote;

        /// <summary>
        /// Socket deal with communication.
        /// </summary>
        public Socket client;

        StateObject state;

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);


        /// <summary>
        /// Connect and test connection
        /// </summary>
        /// <param name="serverIP">The IP address of the server, 
        /// should be the local IP if used as Unity interface</param>
        public MiddleVRPNClient(Remote remote, string addressServer = "127.0.0.1", int port = 8881)
        {
            this.remote = remote;
            try
            {
                //Create a client socket
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                IPEndPoint serverMachine = new IPEndPoint(IPAddress.Parse(addressServer), port);
                client.BeginConnect(serverMachine, new AsyncCallback(ConnectCallback), client);
               /// connectDone.WaitOne();
                DateTime connectionTime = DateTime.Now;
                string peripheral = remote.Name + SEPARATOR + remote.GetHandlerType().ToString();

                //Wait that the client is connected
                //connectDone.WaitOne();  <-- don't work as expected
                while (!client.Connected)
                {
                    Thread.Sleep(1);
                    DateTime currentTime = DateTime.Now;
                    TimeSpan diff = currentTime.Subtract(connectionTime);
                    if (diff.TotalSeconds > 1)
                    {
                        string err = "Client failed to connect to server. Is your IP correct?"
                                        + "Is your UniVRPNity server working?\n";
                        Console.WriteLine(err);
                        throw new TimeoutException(err);
                    }
                }

                Send(client, peripheral);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState; // Retrieve the socket from the state object.
                client.EndConnect(ar); // Complete the connection.
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
                connectDone.Set(); // Signal that the connection has been made.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Receive()
        {
            try
            {
                // Create the state object.
                state = new StateObject();
                state.WorkSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            //UnityEngine.Debug.Log("Receive callback" + this.remote.Name + " of type " + this.remote.GetHandlerType());

            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.WorkSocket;
                

            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);
            this.Receive(); //Always receive.
            if (bytesRead > 0)
            {
                this.deserializeEvent(state, bytesRead);
                // Signal that all bytes have been received.
                receiveDone.Set();
            }           
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected void deserializeEvent(StateObject state, int bytesRead)
        {
            //Receive message from the server
            formatter = new BinaryFormatter();
            memStream = new MemoryStream(bytesRead - 1);
            memStream.Write(state.Buffer, 0, bytesRead - 1);
            memStream.Position = 0;
            UniVRPNity.Event event_deserialize = (UniVRPNity.Event)formatter.Deserialize(memStream);

            switch (event_deserialize.GetHandlerType())
            {
                case UniVRPNity.Type.Analog:
                    analogEvent = (AnalogEvent) event_deserialize;
                    bufferEvent.Push(analogEvent);
                    break;
                case UniVRPNity.Type.Button:
                    buttonEvent = (ButtonEvent) event_deserialize;
                    bufferEvent.Push(buttonEvent);
                    break;
                case UniVRPNity.Type.Tracker:
                    trackerEvent = (TrackerEvent) event_deserialize;
                    bufferEvent.Push(trackerEvent);
                    break;
                default:
                    throw new Exception("Deserialization of the event failed.\n");
            }
        }

        public void Quit()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public BufferEvent BufferEvent
        {
            get
            {
                return this.bufferEvent;
            }
        }

        public static void Main()
        {
            Console.WriteLine("Mouse0 Client");
            AnalogRemote analog = new AnalogRemote("Mouse0@localhost", "127.0.0.1");

            while (true)
            {
                analog.Mainloop();
                Thread.Sleep(1);
            }
        }
    }
}
