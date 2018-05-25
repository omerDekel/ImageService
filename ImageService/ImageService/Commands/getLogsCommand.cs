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
using ImageService.Logging;

namespace ImageService.Commands
{
    class GetLogsCommand : ICommand
    {
        private LogingBuffer logsBuffer;
        private ILoggingService logger;

        public GetLogsCommand(LogingBuffer buffer, ILoggingService logging)
        {
            this.logger = logging;
            this.logsBuffer = buffer;
        }

        /// <summary>
        /// This function execute the command .
        /// </summary>
        /// <param name="args"> arguments for the command </param> 
        /// <param name="result"> result , if the command executed or failed .</param>
        /// <returns> the message of the path , if the command succeed or the exception message if the function failed</returns>
        public string Execute(string[] args, out bool result)
        {
            int logNumber;
            if (int.TryParse(args[0], out logNumber))
            {
                ArrayList logsArray = this.logsBuffer.getLogsFromNumber(logNumber, this.logger);
                if (logsArray.Count < 50)
                {
                    result = true;
                } else
                {
                    result = false;
                }

                string[] logs = new string[logsArray.Count * 2];

                for (int i = 0; i < logsArray.Count; i++)
                {
                    logs[2 * i] = (logsArray[i] as MessageRecievedEventArgs).Status.ToString();
                    logs[2 * i + 1] = (logsArray[i] as MessageRecievedEventArgs).Message;
                }

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
