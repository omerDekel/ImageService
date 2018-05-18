using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    class Server
    {
        private string ipAdd;
        private int port;
        private IPEndPoint ep;
        private TcpListener listener;//,nmhjvfjyhtf
        private IClientHandler handler;

        public Server(string ip, int port, IClientHandler clientHandler)
        {
            this.ipAdd = ip;
            this.port = port;
            this.ep = new IPEndPoint(IPAddress.Parse(this.ipAdd), this.port);
            this.listener = new TcpListener(this.ep);
            this.handler = clientHandler;
        }

        public void StartListening()
        {
            this.listener.Start();
        }

        public void AcceptClients()
        {
            try {
                // Gets a new client.
                TcpClient client = this.listener.AcceptTcpClient();
                this.handler.HandleClient(client);
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StopListening()
        {
            this.listener.Stop();
        }
    }
}
