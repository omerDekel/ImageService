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

        public RemoveDirectoryFromConfigurationCommand(ConfigurationModal configModal)
        {
            this.config = configModal;
        }

        public string Execute(string[] args, out bool result)
        {
            result = this.config.RemoveDirectoryFromList(args[0]);
            return "";
        }

    }
}
