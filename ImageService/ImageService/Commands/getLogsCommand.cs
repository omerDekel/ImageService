using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Logging;
using System.Collections;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;

namespace ImageService.Commands
{
    class GetLogsCommand : ICommand
    {
        private LogingBuffer logsBuffer;
        private ILoggingService logger;

        /// <summary>
        /// A constractor of the get logs command.
        /// </summary>
        /// <param name="buffer"> The logging buffer the command will get the logs from</param>
        /// <param name="logging"></param>
        public GetLogsCommand(LogingBuffer buffer, ILoggingService logging)
        {
            this.logger = logging;
            this.logsBuffer = buffer;
        }

        /// <summary>
        /// This function execute the command .
        /// </summary>
        /// <param name="args"> arguments for the command, contains the log number. </param> 
        /// <param name="result"> result , if the command executed or failed .</param>
        /// <returns> A string represents all the logs fro, the log number the function recives.</returns>
        public string Execute(string[] args, out bool result)
        {
            int logNumber;
            if (int.TryParse(args[0], out logNumber))
            {
                // Gets all the logs from the logging buffer, from the logNumber' to the last log
                ArrayList logsArray = this.logsBuffer.getLogsFromNumber(logNumber, this.logger);

                int size;
                // If the number of logs recived is over 50, return only the fisrt 50.
                if (logsArray.Count < 50)
                {
                    size = logsArray.Count;
                    result = true;
                } else
                {
                    size = 50;
                    result = false;
                }

                
                string[] logs = new string[size * 2];

                // Make a string reepresentatiln of the logs list
                for (int i = 0; i < size; i++)
                {
                    logs[2 * i] = (logsArray[i] as MessageRecievedEventArgs).Status.ToString();
                    logs[2 * i + 1] = (logsArray[i] as MessageRecievedEventArgs).Message;
                }

                // Returns the logs list a a command recived aregs contains a stribg representatiln of the logs list.
                CommandRecievedEventArgs commandRecievedEvent = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, logs, "");

                string str = JsonConvert.SerializeObject(commandRecievedEvent);
                return str;
            }
            else
            {
                result = false;
                return "";
            }
        }
    }
}
