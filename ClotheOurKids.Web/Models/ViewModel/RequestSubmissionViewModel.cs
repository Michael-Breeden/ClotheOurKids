using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestSubmissionViewModel
    {

        //Requestor
        public string Email { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Office { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactMethod { get; set; }

        public string IsVerified { get; set; }

        
        //Request
        public int RequestNumber { get; set; }

        public string Gender { get; set; }

        public string School { get; set; }

        public string Grade { get; set; }

        public string Urgency { get; set; }      

        public string ShirtSize { get; set; }

        public string PantSize { get; set; }

        public string PantLengthSize { get; set; }

        public string UnderwearSize { get; set; }

        public string ShoeSize { get; set; }

        public string Comments { get; set; }


        [Display(Name = "Shirt")]
        public bool NeedShirt { get; set; }

        [Display(Name = "Pants")]
        public bool NeedPant { get; set; }

        [Display(Name = "Underwear")]
        public bool NeedUnderwear { get; set; }

        [Display(Name = "Socks")]
        public bool NeedSocks { get; set; }

        [Display(Name = "Shoes")]
        public bool NeedShoes { get; set; }

        [Display(Name = "Coat")]
        public bool NeedCoat { get; set; }

        [Display(Name = "Hygiene Kit")]
        public bool NeedHygiene { get; set; }

    }
}