using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Gui.Model
{
    class Client
    {
        // Members
        private IPEndPoint ep;
        private TcpListener listener;

        public Client(string ipAdress, int port)
        {
            this.ep = new IPEndPoint(IPAddress.Parse(ipAdress), port);
            this.listener = new TcpListener(ep);
        }
    }
}
