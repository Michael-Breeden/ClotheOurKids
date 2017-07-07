using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestFormViewModel
    {
        public RequestFormViewModel()
        {
            AvailableGenders = new List<SelectListItem>();
            AvailableSchools = new List<SelectListItem>();
            AvailableGrades = new List<SelectListItem>();
            AvailableUrgencies = new List<SelectListItem>();
            AvailableShirtSizes = new List<SelectListItem>();
            AvailablePantSizes = new List<SelectListItem>();
        }

        [Required]
        [Display(Name = "Gender")]
        public string GenderId { get; set; }
        public IList<SelectListItem> AvailableGenders { get; set; }

        [Required]
        [Display(Name = "School")]
        public short SchoolId { get; set; }
        public IList<SelectListItem> AvailableSchools { get; set; }

        [Required]
        [Display(Name = "Grade")]
        public short GradeId { get; set; }
        public IList<SelectListItem> AvailableGrades { get; set; }

        [Required]
        [Display(Name = "Urgency")]
        public byte UrgencyId { get; set; }
        public IList<SelectListItem> AvailableUrgencies { get; set; }

        [Display(Name = "Shirt Size")]
        public Nullable<int> ShirtSizeId { get; set; }
        public IList<SelectListItem> AvailableShirtSizes { get; set; }

        [Display(Name = "Pant Size")]
        public Nullable<int> PantSizeId { get; set; }
        public IList<SelectListItem> AvailablePantSizes { get; set; }

        [Display(Name = "Underwear Size")]
        public string UnderwearSize { get; set; }

        [Display(Name = "Shoe Size")]
        public string ShoeSize { get; set; }

        [Display(Name = "Additional Information")]
        public string Comments { get; set; }

    }
}