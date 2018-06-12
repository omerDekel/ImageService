using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PhotosController : Controller
    {
        static Config configModel = Config.Instance;
        static Photos PhotosModel = new Photos();
        // GET: Photos
        public ActionResult PhotosView()
        {
            PhotosModel.GetPictures(configModel.OutputDirectory);
            return View(PhotosModel);
        }
        /// <summary>
        /// view of full picture .
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult OnePictureView(string path)
        {
            Picture picture = PhotosModel.GetPictureFromPath(path);
            return View(picture);
        }

        public ActionResult DeletePhotoConfirmation(string path)
        {
            Picture picture = PhotosModel.GetPictureFromPath(path);
            return View(picture);
        }
        

        public ActionResult DeleteAprroved(string path)
        {
            Picture picture = PhotosModel.GetPictureFromPath(path);
            PhotosModel.RemovePhoto(picture);
            try
            {
                System.IO.File.Delete(picture.Path);
                System.IO.File.Delete(picture.ThumbPath);
            }
            catch
            {

            }
            return RedirectToAction("PhotosView");
        }
    }
}