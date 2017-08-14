using ClotheOurKids.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class WhoWeServeViewModel
    {
        public IList<Office> AvailableOffices { get; set; }
    }
}