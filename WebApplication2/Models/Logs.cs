using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageService.Logging.Modal;
using WebApplication2.Comunication;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;

namespace WebApplication2.Models
{
  /*  public class Logs
    {
        private IClient client;
        //   public List<LogTypeAndMessage> logArr { get; }

        public Logs()
        {
            /*
            this.logArr = new List<LogTypeAndMessage>();

            // Add a dummy logs just for cheking
            LogTypeAndMessage m = new LogTypeAndMessage();
            m.Message = "massage1";
            m.Type = "INFO";
        //    this.logArr.Add(m);

            m = new LogTypeAndMessage();
            m.Message = "massage2";
            m.Type = "FAIL";
            this.logArr.Add(m);

            this.client = Client.Instance;
            this.client.CommandRecieved += this.OnCommandRecieved;
            
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {/*
            // if it's get log command
            if (e.CommandID == (int)CommandEnum.LogCommand)
            {
                // translate the Args array of CommandRecievedEventArgs to logs and add them to logCollection
                for (int i = 0; i < e.Args.Length; i += 2)
                {
                    LogTypeAndMessage log = new LogTypeAndMessage();
                    log.Type = e.Args[i];
                    log.Message = e.Args[i + 1];
                    this.logArr.Add(log); 
                }
            }
        }
        }
    }*/
}