using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        //private logger
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        // prototype
        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize)
        {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
        }

        public string AddFile(string path, out bool result)
        {
            try
            {
                Directory.CreateDirectory(this.m_OutputFolder);
                DateTime dateTime = File.GetLastWriteTime(path);
                //create directory with the date creation year 
                Directory.CreateDirectory(m_OutputFolder + "\\" + dateTime.Year);
                String newPath = m_OutputFolder + "\\" + dateTime.Year + "\\" + dateTime.Month;
                // create directory with the date creation month 
                Directory.CreateDirectory(newPath);
                String fileName = Path.GetFileName(path);
                File.Move(path, newPath + "\\" + fileName);
                // creating the thumbnail of the photo
                Image image = Image.FromFile(newPath + "\\" + fileName);
                Image thumbnail = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                string thumbPath = m_OutputFolder + "\\" + "Thumbnails\\" + dateTime.Year + "\\" + dateTime.Month;
                // creating the thumbnail directory with year craetion date inside directory (or not if it's allready exist) .
                Directory.CreateDirectory(thumbPath);
                // saving the thumbnail.
                thumbnail.Save(Path.ChangeExtension(thumbPath + "\\" + fileName,"thumb"+fileName));
                result = true;
                return "A file wa added added" + newPath + " year:" + dateTime.Year +"month "+ dateTime.Month;
            } catch (Exception e) {
                result = false;
                // return the exception message .
                return "The file adding failed " + e.Message;
            }

        }

        #endregion

    }
}
