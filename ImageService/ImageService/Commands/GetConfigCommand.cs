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

        /// <summary>
        /// This is the constractor of GetConfigCommand. 
        /// </summary>
        /// <param name="configModal">The configuration model that stores the conciguration, this command will work with.</param>
        public GetConfigCommand(ConfigurationModal configModal)
        {
            this.config = configModal;
        }

        /// <summary>
        /// The function executes the command.
        /// </summary>
        /// <param name="args">The arguments to the command, this commans ignores this parameter.</param>
        /// <param name="result"> result , if the command executed or failed.</param>
        /// <returns>A string representation of the configurations.</returns>
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
