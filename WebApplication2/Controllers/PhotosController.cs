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
        static Config configModel = new Config();
        static Photos PhotosModel = new Photos();
        // GET: Photos
        public ActionResult PhotosView()
        {
            PhotosModel.GetPictures(configModel.OutputDirectory);
            return View(PhotosModel);
        }
    }
}