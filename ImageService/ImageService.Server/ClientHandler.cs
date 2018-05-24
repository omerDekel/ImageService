using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ImageService.Controller;
using System.IO;
using Newtonsoft.Json;
using ImageService.Modal;

namespace ImageService.ImageService.Server
{

    class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IImageController controller;
        private Task comuniationTask;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private int logNum;

        public ClientHandler(IImageController imageController)
        {
            this.client = null;
            this.stream = null;
            this.reader = null;
            this.writer = null;
            this.controller = imageController;
            logNum = 0;
        }

        public void HandleClient(TcpClient client)
        {
            logNum = 0;
            this.client = client;
            this.stream = this.client.GetStream();
            this.reader = new BinaryReader(this.stream);
            this.writer = new BinaryWriter(this.stream);

            // Set the task that will handle the cumunication.
            this.comuniationTask = new Task(this.HandleClient);
            comuniationTask.Start();
        }

        private void HandleClient()
        {
            bool result;
            
            // Wait for the first command. would be get configuration.
            string command = this.reader.ReadString();
            CommandRecievedEventArgs args = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(command);
            string answer = this.controller.ExecuteCommand(args.CommandID, args.Args, out result);
            this.writer.Write(answer);
        }
    }
}
