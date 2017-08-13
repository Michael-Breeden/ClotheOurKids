using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClotheOurKids.Web.Models.ViewModel;
using ClotheOurKids.Model;

namespace ClotheOurKids.Web.Controllers
{
    
    public class HomeController : Controller
    {

        private IHomeRepository _repository;

        public HomeController()
        {
            this._repository = new HomeRepository(new ClotheOurKidsEntities());
        }

        [Route("", Name = "Home")]
        public ActionResult Index()
        {
            var model = new HomeViewModel();

            model.RequestCount = _repository.CountRequests();

            ViewBag.IsHome = true;

            return View(model);
        }


        


        [Route("FAQ", Name = "FAQ")]
        public ActionResult FAQ()
        {
            return View();
        }



    }
}
