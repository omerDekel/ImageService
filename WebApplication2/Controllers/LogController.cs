using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LogController : Controller
    {
        static Logs logsList = new Logs();

        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<LogTypeAndMessage> GetLogs()
        {
            return logsList.logArr;
        }
    }
}