using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;
using System.Net;
using System.Net.Sockets;

namespace ImageService.ImageService.Server
{
    class ClientsManager : IClientHandler
    {
        private IImageController controller;


        public ClientsManager(IImageController control)
        {
            this.controller = control;
        }

        public void HandleClient(TcpClient client)
        {
            //  Creates a new client handler and let it handle the client
            ClientHandler clientHandler = new ClientHandler(this.controller);
            clientHandler.HandleClient(client);
        }
    }
}
