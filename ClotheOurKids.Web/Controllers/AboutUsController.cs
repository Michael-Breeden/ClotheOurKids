using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClotheOurKids.Web.Models;
using ClotheOurKids.Web.Models.ViewModel;
using ClotheOurKids.Model;

namespace ClotheOurKids.Web.Controllers
{
    
    public class AboutUsController : Controller
    {

        private IWhoWeServeRepository _repository;

        public AboutUsController()
        {
            this._repository = new WhoWeServeRepository(new ClotheOurKidsEntities());
        }
        
        // GET: AboutUs  
        public ActionResult Index()
        {
            return View();
        }

        //[Route("Who-We-Are", Name = "WhoWeAre")]
        //public ActionResult WhoWeAre()
        //{
        //    return View();
        //}

        [Route("Our-History", Name = "OurHistory")]
        public ActionResult OurHistory()
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
            var model = new WhoWeServeViewModel();

            model.AvailableOffices = _repository.GetAllOffices();

            return View(model);
        }
    }
}