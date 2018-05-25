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

        public ArrayList getLogsFromNumber(int number, ILoggingService logger)
        {
            logger.Log("In getLodsfromNumber in loggongBuffer",MessageTypeEnum.INFO);
            ArrayList logsToRrturn = new ArrayList();
            if(this.logsList.Count - number > 50)
            {
                logger.Log("logsList.Count - number > 50", MessageTypeEnum.INFO);
                logsToRrturn.AddRange(this.logsList.GetRange(number, number + 50));
            } else
            {
                logger.Log("logsList.Count - number < 50", MessageTypeEnum.INFO);
                logger.Log("get logs from number " + number + "to number" + logsList.Count, MessageTypeEnum.INFO);
                logsToRrturn.AddRange(this.logsList.GetRange(number, logsList.Count -1));
            }
            logger.Log("In logging buffer, got thge ranges of logs ffrom the array", MessageTypeEnum.INFO);
            return logsToRrturn;
        }
    }
}
