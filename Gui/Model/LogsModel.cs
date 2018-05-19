using ImageService.Logging.Modal;
using System.Collections.ObjectModel;
namespace Gui.Model
{
    class LogsModel
    {
        private ObservableCollection<MessageRecievedEventArgs> logsCollection;
        public LogsModel()
        {
            logsCollection = new ObservableCollection<MessageRecievedEventArgs>();
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.WARNING, Message = "hiiiii" };
            MessageRecievedEventArgs messaeRecievedEventArgs = new MessageRecievedEventArgs() { Status = MessageTypeEnum.INFO, Message = "hiiiii" };

            //messageRecievedEventArgs.Message = "hiiiii";
            //messageRecievedEventArgs.Status = 0;
            LogsCollection.Add(messageRecievedEventArgs);
            LogsCollection.Add(messaeRecievedEventArgs);

        }

            
        public ObservableCollection<MessageRecievedEventArgs> LogsCollection
        {
            get { return logsCollection; }
            set { }
        }
    }
}
