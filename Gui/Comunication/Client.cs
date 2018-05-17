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
using System.Threading.Tasks;

namespace Gui.Comunication
{
    class Client : IClient
    {
        private TcpClient tcpClient;
        private TcpListener listener;
        private bool stop;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved

        /// <summary>
        /// 
        /// </summary>
        /// 
        public Client()
        {
            ConnectServer();
        }
        public void CommandFromServer()
        {
            string commandStr;
            new Task(() =>
            {
                while (!stop)
                {
                    NetworkStream stream = tcpClient.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    //BinaryWriter writer = new BinaryWriter(stream);
                    commandStr = reader.ReadString();
                    Console.WriteLine($"Recieve {commandStr} from Server");
                    //string jStr = JsonConvert.SerializeObject(e);
                    //writer.Write(jStr);
                    CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandStr);
                    CommandRecieved?.Invoke(this, responseObj);

                }
            }).Start();
        }

        public void CommandToServer(CommandRecievedEventArgs e)
        {
            new Task(() =>
            {
                NetworkStream stream = tcpClient.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                string jStr = JsonConvert.SerializeObject(e);
                Console.WriteLine($"Send {jStr} to Server");
                writer.Write(jStr);
            }).Start();
        }

        public void ConnectServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ep);
            Console.WriteLine("You are connected");
            stop = false;
        }
        public void Closing()
        {

        }
    }
}
