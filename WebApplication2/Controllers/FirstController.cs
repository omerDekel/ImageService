using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
/// <summary>
/// Controller resposible on configuration and logs screen. 
/// </summary>
namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        //model of this controller .
        static Config configModel = Config.Instance;
        /// <summary>
        /// constructor .
        /// </summary>
        public FirstController()
        {
            configModel.PropertyChanged += OnPropertyChanged;
        }
        /// <summary>
        /// function will be called on OnPropertyChanged event .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// view screen to ask the user if sure to delete the directory handler .
        /// </summary>
        /// <param name="chosenDir">handler directory to delete</param>
        /// <returns></returns>
        public ActionResult DeleteHandlerUserApproval(string chosenDir)
        {
            configModel.ChosenDir = chosenDir;
            return View(configModel);
            
        }
        /// <summary>
        /// going back the main screen of config .
        /// </summary>
        /// <returns> main screen of config </returns>
        public ActionResult ConfigBack()
        {
            return View(configModel);
        }
        /// <summary>
        /// deleting directory handler.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteApproved()
        {
            configModel.DeleteDirectoryHandler();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Log()
        {
            return View(configModel);
        }
    }
}
