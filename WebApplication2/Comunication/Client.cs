using ImageService.Controller;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication2.Comunication
{
    class Client : IClient
    {
        private static Mutex mutex;
        private TcpClient tcpClient;
        private TcpListener listener;
        private bool stop;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        private static Client instance;
        /// <summary>
        /// constructor .
        /// </summary>
        /// 
        public Client()
        {
            ConnectServer();
        }
        /// <summary>
        /// instance for creating singleton of the client.
        /// </summary>
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }
        public bool Stop
        {
            get
            {
                return this.stop;
            }
        }
        /// <summary>
        /// OneCommandFromServer.
        /// function for  handle only one event that command have been recieved .
        /// </summary>
        public void OneCommandFromServer()
        {
            string commandStr;
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                commandStr = reader.ReadString();
                Console.WriteLine($"Recieve {commandStr} from Server");
                CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandStr);
                CommandRecieved?.Invoke(this, responseObj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// task that listenning to the server to get command .
        /// </summary>
        public void CommandFromServer()
        {
            string commandStr;
            new Task(() =>
            {
                Console.WriteLine("listening to the server ");
                while (!stop)
                {
                    try
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        commandStr = reader.ReadString();
                        Console.WriteLine($"Recieve {commandStr} from Server");
                        CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandStr);
                        CommandRecieved?.Invoke(this, responseObj);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }).Start();
        }
        /// <summary>
        /// sending command to the server .
        /// </summary>
        /// <param name="e">arguments for the recieved command</param>
        public void CommandToServer(CommandRecievedEventArgs e)
        {
            Console.WriteLine("Send command to server by a diffrent task");

            new Task(() =>
            {
                try
                {
                    NetworkStream stream = tcpClient.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    string jStr = JsonConvert.SerializeObject(e);
                    Console.WriteLine(jStr);
                    Console.WriteLine($"Send {jStr} to Server");
                    writer.Write(jStr);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.Source);
                }
            }).Start();
        }
        /// <summary>
        /// connecting the client to server .
        /// </summary>
        public void ConnectServer()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                this.tcpClient = new TcpClient();
                this.tcpClient.Connect(ep);
                Console.WriteLine("You are connected");
                stop = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                stop = true;
            }
        }

    }
}
