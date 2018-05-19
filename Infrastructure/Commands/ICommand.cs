using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// interface ICommand . interface for command to exexute.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// This function execute the command .
        /// </summary>
        /// <param name="args"> arguments for the command </param> 
        /// <param name="result"> result , if the command executed or failed .</param>
        /// <returns>string </returns> 
        string Execute(string[] args, out bool result);          // The Function That will Execute The 
    }
}
