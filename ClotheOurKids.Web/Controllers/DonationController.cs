using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;
using System.Web.Routing;

namespace ClotheOurKids.Web.Controllers
{
    public class DonationController : Controller
    {

                // GET: Donate
        public ActionResult Index()
        {
            return View();
        }


        [Route("Donate", Name = "Donate")]
        public ActionResult Donate()
        {
            return View();
        }



        [Route("Get-Involved", Name = "GetInvolved")]
        public ActionResult GetInvolved()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(FormCollection form)
        {
            var myCharge = new StripeChargeCreateOptions();

            myCharge.Amount = Convert.ToInt32(form["amount"]);
            myCharge.Currency = "usd";

            myCharge.Description = "Donation to Clothe Our Kids of Morgan County";

            myCharge.SourceTokenOrExistingSourceId = form["stripeToken"];

            myCharge.Capture = true;

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);

            return RedirectToAction("DonationComplete", new RouteValueDictionary { { "chargeId", stripeCharge.Id } });
        }

        [Route("Thank-You", Name = "DonationComplete")]
        public ActionResult DonationComplete(int chargeId)
        {
            return View();
        }

    }
}