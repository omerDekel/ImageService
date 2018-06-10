using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageService.Logging.Modal;
using WebApplication2.Comunication;

namespace WebApplication2.Models
{
    public class Logs
    {
        private IClient client;
        public List<MessageRecievedEventArgs> logArr { get; }

        public Logs()
        {
            this.logArr = new List<MessageRecievedEventArgs>();

            // Add a dummy logs just for cheking
            MessageRecievedEventArgs m = new MessageRecievedEventArgs();
            m.Message = "massage1";
            m.Status = MessageTypeEnum.INFO;
            this.logArr.Add(m);

            m = new MessageRecievedEventArgs();
            m.Message = "massage2";
            m.Status = MessageTypeEnum.FAIL;
            this.logArr.Add(m);
        }
    }
}