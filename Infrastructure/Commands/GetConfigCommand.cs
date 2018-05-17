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
                args[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                args[1] = ConfigurationManager.AppSettings.Get("SourceName");
                args[2] = ConfigurationManager.AppSettings.Get("LogName");
                args[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                result = true;
                commandRecievedEvent = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, args, "");
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
