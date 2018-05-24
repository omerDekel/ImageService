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
using ImageService.Infrastructure.Enums;

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
        private 
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
            
            // Send the client all the cueent information about the service.

            // Wait for the first command. would be get configuration.
            string command = this.reader.ReadString();
            CommandRecievedEventArgs args = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(command);
            string answer = this.controller.ExecuteCommand(args.CommandID, args.Args, out result);
            this.writer.Write(answer);

            // Send the logs to the client.
            bool isMoreLogs = false;
            while (!isMoreLogs)
            {
                string[] argsToCommand = new string[1];
                argsToCommand[0] = this.logNum.ToString();
                CommandRecievedEventArgs evantArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, argsToCommand, "");
                answer = this.controller.ExecuteCommand(evantArgs.CommandID, evantArgs.Args, out isMoreLogs);
                if (answer.Equals(""))
                {
                    break;
                }
                this.logNum += 50;
                this.writer.Write(answer);
            }

            // Set the logging listener.

        }
    }
}
