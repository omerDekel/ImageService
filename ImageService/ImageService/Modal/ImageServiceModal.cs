﻿using ImageService.Infrastructure;
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

        public ImageServiceModal()
        {
        }

        public string AddFile(string path, out bool result)
        {
            Directory.CreateDirectory(path);
            DateTime dateTime = File.GetCreationTime(path);
            Directory.CreateDirectory(m_OutputFolder + "\\" + dateTime.Year + "\\" + dateTime.Month);
            throw new NotImplementedException();
        }

        #endregion

    }
}
