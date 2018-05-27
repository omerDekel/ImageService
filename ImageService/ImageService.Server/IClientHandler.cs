using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ImageService.Server
{
    public interface IClientHandler
    {
        /// <summary>
        /// The function handles a client comunication.
        /// </summary>
        /// <param name="client">The TcpClient the fuction will handle the comuniation of.</param>
        void HandleClient(TcpClient client);
    }
}
