using ImageService.Controller;
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

        public void CommandFromServer()
        {
            throw new NotImplementedException();
        }

        public void CommandToServer(CommandRecievedEventArgs e)
        {
            new Task(() =>
            {
                NetworkStream stream = tcpClient.GetStream();
                BinaryReader reader = new BinaryReader(stream);
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
            Console.WriteLine("You are connected");        }
    }
}
