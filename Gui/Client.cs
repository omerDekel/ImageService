using ImageService.Controller;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gui.Comunication
{
    class Client : IClient
    {
        private static Mutex mutex;
        private TcpClient tcpClient;
        private TcpListener listener;
        private bool stop;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        private static Client instance;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public Client()
        {
            ConnectServer();
            //CommandFromServer();
        }
                public static Client Instance
        {
            get
            {
               if (instance == null)
               {
                   instance = new Client();
               }
               return instance;
           }
        }
        public bool Stop
        {
            get
            {
                return this.stop;
            }
        }
        public void CommandFromServer()
        {
            string commandStr;
            new Task(() =>
            {
                Console.WriteLine("listening to the server ");
                while (!stop)
                {
                    try
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        commandStr = reader.ReadString();
                        Console.WriteLine($"Recieve {commandStr} from Server");
                        CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(commandStr);
                        CommandRecieved?.Invoke(this, responseObj);
                    } catch (Exception e)
                    {

                    }
                }
            }).Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void CommandToServer(CommandRecievedEventArgs e)
        {
            Console.WriteLine("Send command to server by a diffrent task");
       /*     try
            {
                if (tcpClient == null)
                {
                    Console.WriteLine("Tcp Client is null");
                }
                else
                {
                    Console.WriteLine("Tcp Client is not null");
                }

                NetworkStream stream = tcpClient.GetStream();
                if (stream == null)
                {
                    Console.WriteLine("stream is null");
                }
                else
                {
                    Console.WriteLine("stream Client is not null");
                }

                BinaryWriter writer = new BinaryWriter(stream);

                if (writer == null)
                {
                    Console.WriteLine("writer is null");
                }
                else
                {
                    Console.WriteLine("writer Client is not null");
                }

                if (e == null)
                {
                    Console.WriteLine("command args is null");
                }
                else
                {
                    Console.WriteLine("command args is not null");
                }
                string jStr = JsonConvert.SerializeObject(e);
                Console.WriteLine(jStr);
                Console.WriteLine($"Send {jStr} to Server");
       //         mutex.WaitOne();
                writer.Write(jStr);
         //       mutex.ReleaseMutex();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.Source);

            }*/
            
            new Task(() =>
            {
                try
                {
                    NetworkStream stream = tcpClient.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    string jStr = JsonConvert.SerializeObject(e);
                    Console.WriteLine(jStr);
                    Console.WriteLine($"Send {jStr} to Server");
                    //mutex.WaitOne();
                    writer.Write(jStr);
                    //mutex.ReleaseMutex();
                } catch (Exception exception) {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.Source);
                }
            }).Start();
        }

        public void ConnectServer()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                this.tcpClient = new TcpClient();
                this.tcpClient.Connect(ep);
                Console.WriteLine("You are connected");
                stop = false;
            } catch (Exception e)
            {
                stop = true;
            }
        }
        
    }
}
