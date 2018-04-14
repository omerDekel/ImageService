using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    ///  The Image Processing Controller interface.
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        ///  Executing the Command Requet
        /// </summary>
        /// <param name="commandID"></param>
        /// <param name="args"> arguments to the request</param>
        /// <param name="result">if the command succeed or not .</param>
        /// <returns></returns>
        string ExecuteCommand(int commandID, string[] args, out bool result); 
    }
}
