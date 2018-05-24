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
   public class ComunicationServer
    {
        private string ipAdd;
        private int port;
        private IPEndPoint ep;
        private TcpListener listener;
        private IClientHandler handler;

        public ComunicationServer(string ip, int port, IClientHandler clientHanler)
        {
            this.ipAdd = ip;
            this.port = port;
            this.ep = new IPEndPoint(IPAddress.Parse(this.ipAdd), this.port);
            this.listener = new TcpListener(this.ep);
            this.handler = clientHanler;
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

                // Creates a new client handler and send the client as a task for the client hanbdler.
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
