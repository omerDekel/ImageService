using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        static Config configModel = Config.Instance;
        static Logs logsModel = new Logs();

        public FirstController()
        {
            configModel.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ConfigBack();
        }

        // GET: First
        [HttpGet]
        public ActionResult Index()
        {
            return View(configModel);
        }
        //[HttpGet]
        public ActionResult DeleteHandlerUserApproval(string chosenDir)
        {
            configModel.ChosenDir = chosenDir;
            return View(configModel);
            
        }
        public ActionResult ConfigBack()
        {
            return View(configModel);
        }
        public ActionResult DeleteApproved()
        {
            configModel.DeleteDirectoryHandler();
            //s
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Log()
        {
            return View(logsModel);
        }
    }
}
