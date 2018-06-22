using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService.Modal
{
    class ConfigurationModal
    {
        private string[] configurations;

        /// <summary>
        /// This is constrator of ConfigurationModal, it reads the configuration from the appconfig file
        /// of the service, and stoes the information.
        /// </summary>
        public ConfigurationModal()
        {
            this.configurations = new string[5];
            configurations[0] = ConfigurationManager.AppSettings.Get("OutputDir");
            configurations[1] = ConfigurationManager.AppSettings.Get("SourceName");
            configurations[2] = ConfigurationManager.AppSettings.Get("LogName");
            configurations[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
            configurations[4] = ConfigurationManager.AppSettings.Get("Handler");
        }

        public string[] Configurations
        {
            get
            {
                return configurations;

            }
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// This function gets a directory name and removes the directory from the list of
        /// the directories the service listens to.
        /// </summary>
        /// <param name="dir">A directory name.</param>
        /// <returns></returns>
        public bool RemoveDirectoryFromList(string dir)
        {
            if (this.configurations[4].Contains(dir))
            {
                //const string removeString = dir;
                int index = this.configurations[4].IndexOf(dir);
                int length = dir.Length;
                String startOfString = this.configurations[4].Substring(0, index);
                String endOfString = this.configurations[4].Substring(index + length);
                String cleanPath = startOfString + endOfString;
                cleanPath = cleanPath.Replace(";;", ";");
                if (cleanPath.StartsWith(";"))
                {
                    String temp = cleanPath.Substring(1, cleanPath.Length - 1);
                    cleanPath = temp;
                }
                if (cleanPath.EndsWith(";"))
                {
                    String temp = cleanPath.Substring(0, cleanPath.Length - 1);
                    cleanPath = temp;
                }

                this.configurations[4] = cleanPath;
                return true;
            }
            return false;
        }

        public string getFirstHandlerName()
        {
            string folderName = this.configurations[4].Substring(0, this.configurations[4].IndexOf(";"));
            return folderName;
        }
    }
}

