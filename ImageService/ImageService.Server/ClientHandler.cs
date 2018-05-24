using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ImageService.Controller;

namespace ImageService.ImageService.Server
{

    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IImageController controller;
        private Task comuniationTask;
        int logNum;

        public ClientHandler(IImageController imageController)
        {
            this.client = null;
            this.controller = imageController;
            logNum = 0;
        }

        public void HandleClient(TcpClient client)
        {
            this.client = client;

            // Set the task that will handle the cumunication.
            this.comuniationTask = new Task(this.HandleClient);
            comuniationTask.Start();
        }

        private void HandleClient()
        {
                
        }
    }
}
