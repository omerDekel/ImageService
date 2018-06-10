using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ImageWebController : Controller
    {
        static ImageWeb ImageWebModel = new ImageWeb();
        // GET: First
        [HttpGet]
        public ActionResult ImageWebView()
        {
            return View(ImageWebModel);
        }
    }
}