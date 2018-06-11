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
        static Photos PhotosModel = new Photos();
        // GET: Photos
        public ActionResult PhotosView()
        {
            return View(PhotosModel);
        }
    }
}