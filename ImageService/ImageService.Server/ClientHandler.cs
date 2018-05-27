﻿using System.Threading.Tasks;
using System.Net.Sockets;
using ImageService.Controller;
using System.IO;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Logging;
using ImageService.Controller.Handlers;
using System;

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

        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        public event EventHandler<ClientClosedEventArgs> ClientClosed;

        /// <summary>
        /// This is the constractor of ClientManager.
        /// </summary>
        /// <param name="imageController">The image controller that controls the service data.</param>
        /// <param name="logging">The iloggingSerive that sends thr logs generated by the service.</param>
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

        /// <summary>
        /// This function initiate the comunication with the client.
        /// </summary>
        /// <param name="client">The tcp client, this client handler communicates with.</param>
        public void HandleClient(TcpClient client)
        {
            logNum = 0;
            this.client = client;
            this.stream = this.client.GetStream();
            this.reader = new BinaryReader(this.stream);
            this.writer = new BinaryWriter(this.stream);
            this.isTaskCanceled = false;

            // Set the task that will handle the cumunication.
            this.comuniationTask = new Task(this.HandleClient, new System.Threading.CancellationToken(isTaskCanceled));
            comuniationTask.Start();
        }

        /// <summary>
        /// This funtions handles the comunication with the client.
        /// </summary>
        private void HandleClient()
        {
            bool result;

            // Send the client all the cueent information about the service.

            // Wait for the first command. would be get configuration.
            string command;
            try
            {
                command = this.reader.ReadString();
            }
            catch (IOException e)
            {
                this.CloseConnectionToClient();
                this.logger.Log("Exeption with writing to client at client.HandleClient, disconnectiong from client", MessageTypeEnum.FAIL);
                this.logger.Log(e.Message, MessageTypeEnum.FAIL);
                return;
            }
            
            CommandRecievedEventArgs args = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(command);
            string answer = this.controller.ExecuteCommand(args.CommandID, args.Args, out result);
            try
            {
                this.writer.Write(answer);
            }
            catch (IOException e)
            {
                this.CloseConnectionToClient();
                this.logger.Log("Exeption with writing to client at client.HandleClient, disconnectiong from client", MessageTypeEnum.FAIL);
                this.logger.Log(e.Message, MessageTypeEnum.FAIL);
                return;
            }

            // Send the logs to the client.
            bool isMoreLogs = false;
            while (!isMoreLogs)
            {
                this.logger.Log("Trying to get logs from the buffer", MessageTypeEnum.INFO);
                string[] argsToCommand = new string[1];
                argsToCommand[0] = this.logNum.ToString();
                CommandRecievedEventArgs evantArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, argsToCommand, "");
                answer = this.controller.ExecuteCommand(evantArgs.CommandID, evantArgs.Args, out isMoreLogs);
                if (answer.Equals(""))
                {
                    break;
                }
                this.logNum += 50;

                try
                {
                   this.writer.Write(answer);
                } catch(IOException e)
                {
                    this.CloseConnectionToClient();
                    this.logger.Log("Exeption with writing to client at client.HandleClient, disconnectiong from client", MessageTypeEnum.FAIL);
                    this.logger.Log(e.Message, MessageTypeEnum.FAIL);
                }

            }

            // Set the logging listener.
            this.isListeningToLogger = true;

            while (true)
            {
                this.logger.Log("Start listening to commands from the gui", MessageTypeEnum.INFO);
                //Gets commands and execute them.
                try
                {
                    string commandFromClient = this.reader.ReadString();
                    this.logger.Log("Got a command from the client", MessageTypeEnum.INFO);
                    this.logger.Log("The command is" + commandFromClient, MessageTypeEnum.INFO);
                    CommandRecievedEventArgs commandArgs = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandFromClient);

                    // If the command is a close directory hadler command.
                    if (commandArgs.CommandID == (int)CommandEnum.CloseCommand)
                    {
                        CommandRecieved?.Invoke(this, commandArgs);
                    }
                    else
                    {
                        this.controller.ExecuteCommand(commandArgs.CommandID, commandArgs.Args, out result);
                    }
                }
                catch (IOException e)
                {
                    this.CloseConnectionToClient();
                    this.logger.Log("Exeption with reading from client at client.OnDirectoryClose, disconnecting from client", MessageTypeEnum.FAIL);
                    this.logger.Log(e.Message, MessageTypeEnum.FAIL);
                    
                    return;
                }
            }
        }

        /// <summary>
        /// This functions is being invoked when the logging service send a log.
        /// </summary>
        /// <param name="sender">The logging service that send the log</param>
        /// <param name="args">Event args - contains the log that ilogging service has sent.</param>
        public void ReciveLog(object sender, MessageRecievedEventArgs args)
        {
            if (this.isListeningToLogger)
            {
                string[] logsToSend = new string[2];
                logsToSend[0] = ((int)args.Status).ToString();
                logsToSend[1] = args.Message;
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, logsToSend, "");
                string command = JsonConvert.SerializeObject(commandArgs);
                try
                {
                    this.writer.Write(command);
                }
                catch (IOException e)
                {
                    this.CloseConnectionToClient();
                    this.logger.Log("Exeption with writing to client at client.ReciveLog , disconnectiong from client", MessageTypeEnum.FAIL);
                    this.logger.Log(e.Message, MessageTypeEnum.FAIL);
                }
            }
        }

        /// <summary>
        /// This function is being invoked when a directory handler is closed.
        /// This functin sends a notification the the client that the service has stopped listening 
        /// to the diretory. In addtion the function stops listening from notuffication  from this directory handler.
        /// </summary>
        /// <param name="sender">The directory hadler the was closed</param>
        /// <param name="e">The paremetrs the directoryHanler send to the funtion.</param>
        public void OnDirectoryClose(object sender, DirectoryCloseEventArgs e)
        {
            // Remove the directory from the event of "CommandRecieved" of this client
            this.CommandRecieved -= ((DirectoyHandler)sender).OnCommandRecieved;

            this.logger.Log("A client get that a directory was closed", MessageTypeEnum.INFO);
            CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, e.DirectoryPath);
            string command = JsonConvert.SerializeObject(commandArgs);
            try
            {
                this.writer.Write(command);
            }
            catch (IOException ex)
            {
                this.CloseConnectionToClient();
                this.logger.Log("Exeption with writing to client at client.OnDirectoryClose, disconnectiong from client", MessageTypeEnum.FAIL);
                this.logger.Log(ex.Message, MessageTypeEnum.FAIL);
                return;
            }
            this.logger.Log("An information about directory was closed, has been sent to the client", MessageTypeEnum.INFO);
        }

        /// <summary>
        /// This funtion is called when the client has stoped the communication with the server.
        /// This funtion is closing all the streams was opend to communicat with the client.
        /// </summary>
        private void CloseConnectionToClient()
        {
            this.isListeningToLogger = false;
            this.isTaskCanceled = true;
            this.writer.Close();
            this.reader.Close();
            this.ClientClosed?.Invoke(this, new ClientClosedEventArgs(this));
        }
    }
}
