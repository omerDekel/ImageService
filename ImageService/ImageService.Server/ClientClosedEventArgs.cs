using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ClientClosedEventArgs : EventArgs
    {
        public ClientHandler Client { get; set; }

        /// <summary>
        /// The constrator of ClientClosedEventArgs.
        /// </summary>
        /// <param name="client">The clientHandler that ClientClosedEventArgs holdes.</param>
        public ClientClosedEventArgs(ClientHandler client)
        {
            Client = client;
        }
    }
}
