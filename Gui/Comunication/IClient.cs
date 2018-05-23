using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using ImageService.Modal;

namespace Gui.Comunication
{
    interface IClient
    {
        void ConnectServer();
        void CommandToServer(CommandRecievedEventArgs e);
        void CommandFromServer();
    }
}
