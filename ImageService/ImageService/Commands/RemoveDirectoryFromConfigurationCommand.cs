using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;

namespace ImageService.Commands
{
    class RemoveDirectoryFromConfigurationCommand : ICommand
    {
        private ConfigurationModal config;

        /// <summary>
        /// This is the constractor of RemoveDirectoryFromConfigurationCommand. 
        /// </summary>
        /// <param name="configModal">The configuration model that stores the conciguration, this command will work with.</param>
        public RemoveDirectoryFromConfigurationCommand(ConfigurationModal configModal)
        {
            this.config = configModal;
        }

        /// <summary>
        /// The function executes the command.
        /// </summary>
        /// <param name="args">The arguments to the command, contains the directory name to remove from the handlers list.</param>
        /// <param name="result">> result , if the command executed or failed.</param>
        /// <returns>An empty string</returns>
        public string Execute(string[] args, out bool result)
        {
            result = this.config.RemoveDirectoryFromList(args[0]);
            return "";
        }

    }
}
