using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Controllers
{
    
    public class HomeController : Controller
    {
        [Route("", Name = "Home")]
        public ActionResult Index()
        {
            ViewBag.IsHome = true;

            return View();
        }


        


        [Route("FAQ", Name = "FAQ")]
        public ActionResult FAQ()
        {
            return View();
        }



    }
}
