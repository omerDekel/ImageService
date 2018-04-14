using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// NewFileCommand.  command to add new file  .
    /// </summary>
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;
        /// <summary>
        /// Constructor .
        /// </summary>
        /// <param name="modal">IImageServiceModal </param>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }
        /// <summary>
        /// This function execute the command .
        /// </summary>
        /// <param name="args"> arguments for the command </param> 
        /// <param name="result"> result , if the command executed or failed .</param>
        /// <returns> the message of the path , if the command succeed or the exception message if the function failed</returns>
        public string Execute(string[] args, out bool result)
        {
			// The String Will Return the New Path if result = true, and will return the error message
            return m_modal.AddFile(args[0], out result);
        }
    }
}
