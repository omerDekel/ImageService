using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Photos
    {
        private List<Picture> pictures = new List<Picture>();
        /// <summary>
        /// updating the pictures list according to the parameter of path to directory.
        /// </summary>
        /// <param name="directory">path to directory to take the photos from there</param>
        public void GetPictures(string directory)
        {

            DirectoryInfo di = null;
            Picture pict;
            try
            {
                pictures.Clear();
                di = new DirectoryInfo(directory);
                List<DirectoryInfo> years = di.GetDirectories().ToList();
                foreach (DirectoryInfo year in years)
                {
                    if (year.Name == "Thumbnails")
                    {
                        continue;
                    }
                    List<DirectoryInfo> months = year.GetDirectories().ToList();

                    foreach (DirectoryInfo month in months)
                    {
                        List<FileInfo> fileInfos = month.GetFiles().ToList();
                        foreach (FileInfo fi in fileInfos)
                        {
                            pict = new Picture();
                            pict.RelativePath = "~/" + year.Parent.Name + "/" + year.Name + "/" + month.Name + "/" + fi.Name;
                            pict.RelativeThumbPath = "~/" + year.Parent.Name + "/Thumbnails/" + year.Name + " /" + month.Name + "/" + fi.Name;
                            Path.ChangeExtension(pict.RelativeThumbPath, "thumb" + fi.Name);
                            pict.Path = fi.FullName;
                            pict.ThumbPath = fi.FullName.Replace(pict.RelativePath, pict.RelativeThumbPath);

                            pict.Name = fi.Name;
                            pict.Month = month.Name;
                            pict.Year = year.Name;
                            pictures.Add(pict);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Picture GetPictureFromPath(string relPath)
        {
            foreach ( Picture pict in pictures)
            {
                if (pict.RelativeThumbPath == relPath)
                {
                    return pict;
                }
            }
            return null;
        }
        public void RemovePhoto(Picture picture)
        {
            pictures.Remove(picture);
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Pictures")]
        public List<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}
