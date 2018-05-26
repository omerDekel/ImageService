using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using ImageService.Modal;
using ImageService.Infrastructure.Enums;

namespace ImageService.Commands
{
    class GetConfigCommand : ICommand
    {
        private ConfigurationModal config;

        public GetConfigCommand(ConfigurationModal configModal)
        {
            this.config = configModal;
        }

        public string Execute(string[] args, out bool result)
        {
            try
            {
                string[] argsToCommand = this.config.Configurations;
                CommandRecievedEventArgs commandRecievedEvent = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, argsToCommand, "");
                result = true;
                string jStr = JsonConvert.SerializeObject(commandRecievedEvent);
                return jStr;
            } catch (Exception e)
            {
                result = false;
                return e.ToString();
            }

        }
    }
}
