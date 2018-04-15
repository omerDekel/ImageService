
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
        /// <summary>
        /// The event being raised when a massage is sent.
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        /// <summary>
        /// Gets a message, and sends it to all of the functions registerd to get the massege.
        /// </summary>
        /// <param name="message">The massage to be send.</param>
        /// <param name="type">The message type information/ warning /error</param>
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
