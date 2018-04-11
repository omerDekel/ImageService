
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        public void Log(string message, MessageTypeEnum type)
        {
            // Creates the args calss for the log writing event.
            // and puts the arguments in it.
            MessageRecievedEventArgs arg = new MessageRecievedEventArgs();
            arg.Message = message;
            arg.Status = type;

            // Raise the event.
            this.MessageRecieved?.Invoke(this, arg);
        }
    }
}
