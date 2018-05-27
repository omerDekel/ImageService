using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Logging.Modal;

namespace ImageService.Server
{
   public class ComunicationServer
    {
        private string ipAdd;
        private int port;
        private IPEndPoint ep;
        private TcpListener listener;
        private IClientHandler handler;
        private ILoggingService logger;

        /// <summary>
        /// The constractor of the communiation server.
        /// </summary>
        /// <param name="ip">The ip the server will use</param>
        /// <param name="port">The port the server will use</param>
        /// <param name="clientHanler">The client hadler that will handle the client are conneted to the server.</param>
        /// <param name="loggingService">The logging service the server wull write logs  to.</param>
        public ComunicationServer(string ip, int port, IClientHandler clientHanler, ILoggingService loggingService)
        {
            this.ipAdd = ip;
            this.port = port;
            this.ep = new IPEndPoint(IPAddress.Parse(this.ipAdd), this.port);
            this.listener = new TcpListener(this.ep);
            this.handler = clientHanler;
            this.logger = loggingService;
       }

        /// <summary>
        /// This function make the server start listening to the clients.
        /// </summary>
        public void StartListening()
        {
            this.listener.Start();
            this.logger.Log("The Connserver has start listening to clients", MessageTypeEnum.INFO);
        }

        /// <summary>
        /// This function make the server start acception clients that connects to the server.
        /// </summary>
        public void AcceptClients()
        {
            while (true)
            {
                try
                {
                    // Gets a new client.
                    TcpClient client = this.listener.AcceptTcpClient();

                    // Creates a new client handler and send the client as a task for the client hanbdler.
                    this.handler.HandleClient(client);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
                this.logger.Log("A new client has eceepted", MessageTypeEnum.INFO);
            }
        }

        /// <summary>
        /// Stop listeniong to new clients.
        /// </summary>
        public void StopListening()
        {
            this.listener.Stop();
        }
    }
}
