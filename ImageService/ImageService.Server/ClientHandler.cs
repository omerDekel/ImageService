﻿using System;
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
using ImageService.Logging.Modal;
using ImageService.Logging;

namespace ImageService.Server
{
    public class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IImageController controller;
        private Task comuniationTask;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private int logNum;
        private bool isListeningToLogger;
        private bool isTaskCanceled;
        private ILoggingService logger;
        public ClientHandler(IImageController imageController, ILoggingService logging)
        {
            this.client = null;
            this.stream = null;
            this.reader = null;
            this.writer = null;
            this.controller = imageController;
            this.isListeningToLogger = false;
            this.logger = logging;
            logNum = 0;
        }

        public void HandleClient(TcpClient client)
        {
            logNum = 0;
            this.client = client;
            this.stream = this.client.GetStream();
            this.reader = new BinaryReader(this.stream);
            this.writer = new BinaryWriter(this.stream);
            this.isTaskCanceled = false;

            // Set the task that will handle the cumunication.
            this.comuniationTask = new Task(this.HandleClient, new System.Threading.CancellationToken (isTaskCanceled));
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
                this.logger.Log("Trying to get logs from the buffer", MessageTypeEnum.INFO);
                string[] argsToCommand = new string[1];
                argsToCommand[0] = this.logNum.ToString();
                CommandRecievedEventArgs evantArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, argsToCommand, "");
                this.logger.Log("The event args are" + evantArgs.CommandID.ToString() + ", " + evantArgs.Args[0], MessageTypeEnum.INFO);
                answer = this.controller.ExecuteCommand(evantArgs.CommandID, evantArgs.Args, out isMoreLogs);
                this.logger.Log("The command answer was" + answer , MessageTypeEnum.INFO);
                if (answer.Equals(""))
                {
                    this.logger.Log("Falied to get logs  from the buffer", MessageTypeEnum.WARNING);
                    break;
                }
                this.logger.Log("Logs have been sent to the client", MessageTypeEnum.INFO);
                this.logNum += 50;
                this.writer.Write(answer);
            }

            // Set the logging listener.
            this.isListeningToLogger = true;

            while (true)
            {
                //Gets commands and execute them.
                string commandFromClient = this.reader.ReadString();
                CommandRecievedEventArgs commandArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandFromClient);
                this.controller.ExecuteCommand(commandArgs.CommandID, commandArgs.Args, out result);
            }
        }

        public void ReciveLog(object sender, MessageRecievedEventArgs args)
        {
            if (this.isListeningToLogger)
            {
                string[] logsToSend = new string[2];
                logsToSend[0] = ((int)args.Status).ToString();
                logsToSend[1] = args.Message;
            }
        }

        public void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, e.DirectoryPath);
            string command = JsonConvert.SerializeObject(commandArgs);
            this.writer.Write(command);
        }
    }
}
