using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageService.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private LogingBuffer loggingBuffer;
        private Dictionary<int, ICommand> commands;             // Dictionary from the command id to the command
        private ILoggingService logger; // delete
        public ImageController(IImageServiceModal modal, LogingBuffer logsBuffer, ILoggingService logger)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            this.loggingBuffer = logsBuffer;
            this.logger = logger;
            ConfigurationModal config = new ConfigurationModal();
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int)CommandEnum.NewFileCommand, new NewFileCommand(m_modal) },
                {(int)CommandEnum.LogCommand, new GetLogsCommand(logsBuffer, this.logger)},
                {(int)CommandEnum.GetConfigCommand, new GetConfigCommand(config)},
                {(int)CommandEnum.RemoveDirectoryFromConfigurationCommand, new RemoveDirectoryFromConfigurationCommand(config)}
            };
        }

        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            return commands[commandID].Execute(args, out resultSuccesful);
        }
    }
}
