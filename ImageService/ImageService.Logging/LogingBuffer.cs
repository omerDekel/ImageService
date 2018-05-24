using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using System.Collections;

namespace ImageService.Logging
{
    public class LogingBuffer
    {
        private ArrayList logsList;

        public LogingBuffer()
        {
            this.logsList = new ArrayList();
        }

        public LogingBuffer(List<MessageRecievedEventArgs> copy)
        {
            this.logsList = new ArrayList();
            this.logsList.AddRange(copy);
        }

        public ArrayList getLogsList()
        {
            return this.logsList;
        }

        public ArrayList FlushlogsList()
        {
            ArrayList listToReturn = this.logsList;
            this.logsList = new ArrayList();
            return this.logsList;
        }

        public void AddLog(object sender, MessageRecievedEventArgs args)
        {
            this.logsList.Add(args);
        }

        public ArrayList getLogsFromNumber(int number)
        {
            ArrayList logsToRrturn = new ArrayList();
            if(this.logsList.Count - number > 50)
            {
                logsList.AddRange(this.logsList.GetRange(number, number + 50));
            } else
            {
                logsList.AddRange(this.logsList.GetRange(number, logsList.Count -1));
            }
            return logsToRrturn;
        }
    }
}
