using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public interface ILoggingService
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        /// <summary>
        /// Gets a message, and sends it to all of the functions registerd to get the massege.
        /// </summary>
        /// <param name="message">The massage to be send.</param>
        /// <param name="type">The message type information/ warning /error</param>
        void Log(string message, MessageTypeEnum type);
    }
}
