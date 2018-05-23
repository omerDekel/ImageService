using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ImageService.ImageService.Server
{

    class ClientHandler : IClientHandler
    {
        private TcpClient client;

        public ClientHandler(TcpClient clientToHandle)
        {
            this.client = clientToHandle;
        }

        public void HandleClient()
        {

        }
    }
}
