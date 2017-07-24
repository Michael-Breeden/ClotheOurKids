using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Controllers
{
    public class OurWorkController : Controller
    {
        // GET: OurWork
        public ActionResult Index()
        {
            return View();
        }

        [Route("Our-Work", Name = "OurWork")]
        public ActionResult OurWork()
        {
            return View();
        }
    }
}