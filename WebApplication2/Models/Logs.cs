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
        public List<LogTypeAndMessage> logArr { get; }

        public Logs()
        {
            this.logArr = new List<LogTypeAndMessage>();

            // Add a dummy logs just for cheking
            LogTypeAndMessage m = new LogTypeAndMessage();
            m.Message = "massage1";
            m.Type = "INFO";
            this.logArr.Add(m);

            m = new LogTypeAndMessage();
            m.Message = "massage2";
            m.Type = "FAIL";
            this.logArr.Add(m);
        }
    }
}