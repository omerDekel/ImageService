using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;
using ImageService.Logging;
using System.Net;
using System.Net.Sockets;
using ImageService.Controller.Handlers;

namespace ImageService.Server
{
    public class ClientsManager : IClientHandler
    {
        private IImageController controller;
        private ILoggingService logging;
        private List<IDirectoryHandler> handlersList;


        public ClientsManager(IImageController control, ILoggingService logs)
        {
            this.controller = control;
            this.logging = logs;
            this.handlersList = new List<IDirectoryHandler>();
        }

        public void addDirectoryHandler(IDirectoryHandler directory)
        {
            this.handlersList.Add(directory);
        }

        public void HandleClient(TcpClient client)
        {
            //  Creates a new client handler and let it handle the client
            ClientHandler clientHandler = new ClientHandler(this.controller, this.logging);

            // Make the client recive logs.
            this.logging.MessageRecieved += clientHandler.ReciveLog;

            //Make the client get notification when a handler is closed.
            foreach(DirectoyHandler dirHandler in this.handlersList)
            {
                dirHandler.DirectoryClose += clientHandler.OnDirectoryClose;
            }

            clientHandler.HandleClient(client);
        }
    }
}
