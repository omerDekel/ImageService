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

        /// <summary>
        /// This is the constractor of ClientsManager. 
        /// </summary>
        /// <param name="control">The service controler, repsosible to execute the command on the data</param>
        /// <param name="logs">The logging service that send the logs the program generate.</param>
        public ClientsManager(IImageController control, ILoggingService logs)
        {
            this.controller = control;
            this.logging = logs;
            this.handlersList = new List<IDirectoryHandler>();
        }

        /// <summary>
        /// The clientsManager hold a list of the directory hadlers, the service listen to,
        /// this function adds a directoryHandel to the list.
        /// </summary>
        /// <param name="directory">A directory hadler will be added to the list of directory hadlers
        /// the ClientsManager holds.</param>
        public void AddDirectoryHandler(IDirectoryHandler directory)
        {
            this.handlersList.Add(directory);
        }

        /// <summary>
        /// This function handles a cliet that connetes to the server.
        /// </summary>
        /// <param name="client">The TcpClient that connected to the server.</param>
        public void HandleClient(TcpClient client)
        {
            //  Creates a new client handler and let it handle the client
            ClientHandler clientHandler = new ClientHandler(this.controller, this.logging);

            // Make the current client tell the clientManager when it closed
            clientHandler.ClientClosed += this.OnClientClosed;

            // Make the client recive logs.
            this.logging.MessageRecieved += clientHandler.ReciveLog;

            //Make the client get notification when a handler is closed.
            foreach(DirectoyHandler dirHandler in this.handlersList)
            {
                dirHandler.DirectoryClose += clientHandler.OnDirectoryClose;
                clientHandler.CommandRecieved += dirHandler.OnCommandRecieved;
            }

            clientHandler.HandleClient(client);
        }

        /// <summary>
        /// This is a funcrion the being invoked when a client of the server is closed.
        /// </summary>
        /// <param name="sender">The object called the function.</param>
        /// <param name="args">The arguments the function gets.</param>
        public void OnClientClosed(object sender, ClientClosedEventArgs args)
        {
            this.logging.MessageRecieved -= (sender as ClientHandler).ReciveLog;
        }
    }
}
