using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ImageService.Controller;

namespace ImageService.Server
{

    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IImageController controller;
        private int logNumber;
        private Task task;
        private Boolean isRuning;

        public ClientHandler(IImageController control) 
        {
            this.controller = control;
        }

        public void HandleClient(TcpClient clientToHandle)
        {
            this.client = clientToHandle;
            this.logNumber = 0;
            this.isRuning = true;
            // Make the handling happen in an other thread.
            this.task = new Task(this.Handle);
            task.Start();
        }

        public void Handle()
        {
            // Todo - gets commands from the client desirialize then the send then them to the client
        }

        public void StopHandling()
        {
            this.isRuning = false;
            // todo Stop listening to the hanlers.
        }
    }
}
