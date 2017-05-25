using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestFormModel
    {
        public RequestFormModel()
        {
            AvailableGenders = new List<SelectListItem>();
            AvailablePositions = new List<SelectListItem>();
            AvailableOffices = new List<SelectListItem>();
            AvailableGrades = new List<SelectListItem>();
            AvailableShirtSizes = new List<SelectListItem>();
            AvailablePantSizes = new List<SelectListItem>();
        }


        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public IList<SelectListItem> AvailableGenders { get; set; }

        [Display(Name = "Position")]
        public int PositionId { get; set; }
        public IList<SelectListItem> AvailablePositions { get; set; }

        [Display(Name = "Office")]
        public int OfficeId { get; set; }
        public IList<SelectListItem> AvailableOffices { get; set; }

        [Display(Name = "Grade")]
        public int GradeId { get; set; }
        public IList<SelectListItem> AvailableGrades { get; set; }

        [Display(Name = "ShirtSize")]
        public int ShirtSizeId { get; set; }
        public IList<SelectListItem> AvailableShirtSizes { get; set; }

        [Display(Name = "PantSize")]
        public int PantSizeId { get; set; }
        public IList<SelectListItem> AvailablePantSizes { get; set; }

    }
}