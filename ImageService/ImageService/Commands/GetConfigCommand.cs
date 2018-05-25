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
        public string Execute(string[] args, out bool result)
        {
            CommandRecievedEventArgs commandRecievedEvent;
            try
            {
                string []argsToCommand = new string[5];
                argsToCommand[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                argsToCommand[1] = ConfigurationManager.AppSettings.Get("SourceName");
                argsToCommand[2] = ConfigurationManager.AppSettings.Get("LogName");
                argsToCommand[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                argsToCommand[4] = ConfigurationManager.AppSettings.Get("Handler");
                result = true;
                commandRecievedEvent = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, argsToCommand, "");
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
