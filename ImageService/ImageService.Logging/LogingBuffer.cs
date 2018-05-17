using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using System.Collections;

namespace ImageService.ImageService.Logging
{
    class LogingBuffer
    {
        private List<MessageRecievedEventArgs> logsList;

        public LogingBuffer()
        {
            this.logsList = new List<MessageRecievedEventArgs>();
        }

        public LogingBuffer(List<MessageRecievedEventArgs> copy)
        {
            this.logsList = new List<MessageRecievedEventArgs>();
            this.logsList.AddRange(copy);
        }

        public List<MessageRecievedEventArgs> getLogsList()
        {
            return this.logsList;
        }

        public List<MessageRecievedEventArgs> FlushlogsList()
        {
            List<MessageRecievedEventArgs> listToReturn = this.logsList;
            this.logsList = new List<MessageRecievedEventArgs>();
            return this.logsList;
        }

        public void AddLog(object sender, MessageRecievedEventArgs args)
        {
            this.logsList.Add(args);
        }

    }
}
