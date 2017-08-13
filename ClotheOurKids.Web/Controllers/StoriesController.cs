using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Controllers
{
    public class StoriesController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Route("Stories", Name = "Stories")]
        public ActionResult Stories()
        {
            return View();
        }
    }
}