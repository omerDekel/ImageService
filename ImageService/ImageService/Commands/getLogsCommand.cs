using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Logging;
using System.Collections;

namespace ImageService.Commands
{
    class GetLogsCommand : ICommand
    {
        private LogingBuffer logsBuffer;

        public GetLogsCommand(LogingBuffer buffer)
        {
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
                result = true;
                ArrayList logsArray = this.logsBuffer.getLogsFromNumber(logNumber);
                string str = JsonConvert.SerializeObject(logsArray);
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
