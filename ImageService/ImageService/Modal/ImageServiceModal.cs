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
        #endregion
        // prototype
        public ImageServiceModal(string m_OutputFolder, int m_thumbnailSize)
        {
            this.m_OutputFolder = m_OutputFolder;
            this.m_thumbnailSize = m_thumbnailSize;
        }

        public string AddFile(string path, out bool result)
        {
            Image image = null;
            Image thumbnail = null;
            DirectoryInfo di = null;
            Thread.Sleep(500);
            try
            {
                di =  Directory.CreateDirectory(this.m_OutputFolder);
                di.Attributes |= FileAttributes.Hidden;
                DateTime dateTime = GetDateTakenFromImage(path);
                //create directory with the date creation year 
                Directory.CreateDirectory(m_OutputFolder + "\\" + dateTime.Year);
                String newPath = m_OutputFolder + "\\" + dateTime.Year + "\\" + dateTime.Month;
                // create directory with the date creation month 
                Directory.CreateDirectory(newPath);
                String fileName = Path.GetFileName(path);

                // Trying to get to the file if not taken by other process
                bool isOk = false;
                while (!isOk)
                {
                    try
                    {
                        // move the picture
                        File.Move(path, newPath + "\\" + fileName);
                        isOk = true;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(500);
                    }
                }

                // creating the thumbnail of the photo
                isOk = false;
                while (!isOk)
                {
                    try
                    {
                        image = Image.FromFile(newPath + "\\" + fileName);
                        isOk = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500);
                    }
                }
                // Trying to get to the file if not taken by other process
                isOk = false;
                while (!isOk)
                {
                    try
                    {
                        thumbnail = image.GetThumbnailImage(m_thumbnailSize, m_thumbnailSize, () => false, IntPtr.Zero);
                        isOk = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500);
                    }
                }

                string thumbPath = m_OutputFolder + "\\" + "Thumbnails\\" + dateTime.Year + "\\" + dateTime.Month;
                // creating the thumbnail directory with year craetion date inside directory (or not if it's allready exist) .
                Directory.CreateDirectory(thumbPath);
                // saving the thumbnail.
                isOk = false;
                while (!isOk)
                {
                    try
                    {
                        thumbnail.Save(Path.ChangeExtension(thumbPath + "\\" + fileName, "thumb" + fileName));
                        isOk = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500);
                    }
                }
              
                result = true;
                return "A file wa added added" + newPath + " year:" + dateTime.Year +"month "+ dateTime.Month;
            } catch (Exception e) {
                result = false;
                // return the exception message .
                return "The file adding failed " + path + " " + e.Message;
            } finally
            {
                if (image != null)
                {
                    image.Dispose();
                } if (thumbnail != null)
                {
                    thumbnail.Dispose();
                }
            }
             
        }
        /// <summary>
        /// return the Date the picture was taken .
        /// from https://stackoverflow.com/questions/180030/how-can-i-find-out-when-a-picture-was-actually-taken-in-c-sharp-running-on-vista
        /// </summary>
        /// <param name="path">the path</param>
        /// <returns></returns>
        public static DateTime GetDateTakenFromImage(string path)
        {
            Regex r = new Regex(":");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                myImage.Dispose();
                return DateTime.Parse(dateTaken);
                
            }
        }
    }
}
