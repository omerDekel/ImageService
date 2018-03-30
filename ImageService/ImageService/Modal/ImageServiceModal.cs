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
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize)
        {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
        }

        public string AddFile(string path, out bool result)
        {
            try
            {
                Directory.CreateDirectory(path);
                DateTime dateTime = File.GetCreationTime(path);
                Directory.CreateDirectory(m_OutputFolder + "\\" + dateTime.Year);
                String newPath = m_OutputFolder + "\\" + dateTime.Year + "\\" + dateTime.Month;
                Directory.CreateDirectory(newPath);
                String fileName = Path.GetFileName(path);
                File.Copy(path, newPath + "\\" + fileName);
                Image image = Image.FromFile(path);
                Image thumbnail = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                thumbnail.Save(Path.ChangeExtension(newPath + "\\" + fileName,"thumb"));
                result = true;
                return newPath;
         
            } catch (Exception e)
            {
                result = false;
                return e.Message;
            }

        }

        #endregion

    }
}
