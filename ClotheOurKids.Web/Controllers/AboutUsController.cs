using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Controllers
{
    
    public class AboutUsController : Controller
    {
        // GET: AboutUs  
        public ActionResult Index()
        {
            return View();
        }

        [Route("Who-We-Are", Name = "WhoWeAre")]
        public ActionResult WhoWeAre()
        {
            return View();
        }

        [Route("Leadership", Name = "LeadershipPage")]
        public ActionResult Leadership()
        {
            return View();
        }

        [Route("Who-We-Serve", Name = "WhoWeServe")]
        public ActionResult WhoWeServe()
        {
            return View();
        }


        [Route("What-We-Do", Name = "WhatWeDo")]
        public ActionResult WhatWeDo()
        {
            return View();
        }

        [Route("What-We-Give", Name = "WhatWeGive")]
        public ActionResult WhatWeGive()
        {
            return View();
        }
    }
}