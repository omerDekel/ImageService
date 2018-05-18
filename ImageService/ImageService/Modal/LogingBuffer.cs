using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using System.Collections;

namespace ImageService.Modal
{
    class LogingBuffer
    {
        private ArrayList logsList;
        private int maxLogsForMassage;
        public LogingBuffer(int maxLogsInMassage)
        {
            this.maxLogsForMassage = maxLogsInMassage;
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

        public ArrayList getLogFromLogNumber(int logNumber)
        {
            ArrayList logsArray = new ArrayList();

            // Checks if there are more than 50 logs to retrun
            if (logNumber + this.maxLogsForMassage < this.logsList.Count)
            {
                logsArray.AddRange(this.logsList.GetRange(logNumber, this.maxLogsForMassage));
            } else {
                logsArray.AddRange(this.logsList.GetRange(logNumber, this.logsList.Count - logNumber));
            }
            
            return logsArray;
        }
    }
}
