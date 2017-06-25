using ClotheOurKids.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ClotheOurKids.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Your Position")]
        public short PositionId { get; set; }

        [Required]
        [Display(Name = "Your Employer")]
        public short OfficeId { get; set; }

        [Required]
        [Display(Name = "Preferred Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Best Way to Contact You")]
        public short ContactMethodId { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            AvailableOfficeTypes = new List<SelectListItem>();
            AvailableOffices = new List<SelectListItem>();
            AvailablePositions = new List<SelectListItem>();
            AvailableContactMethods = new List<SelectListItem>();
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "Your password must have: <br/>- 8 or more characters <br/>- 1 number <br/>- 1 uppercase letter <br/>- 1 lowercase letter")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Your Office Type")]
        public int? OfficeTypeId { get; set; }
        public IList<SelectListItem> AvailableOfficeTypes { get; set; }

        [Required]
        [Display(Name = "Your Position")]
        public short? PositionId { get; set; }
        public IList<SelectListItem> AvailablePositions { get; set; }

        [Required]
        [Display(Name = "Your Office")]
        public short? OfficeId { get; set; }
        public IList<SelectListItem> AvailableOffices { get; set; }

        [Required]
        [Display(Name = "Preferred Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Best Way to Contact You")]
        public short? ContactMethodId { get; set; }
        public IList<SelectListItem> AvailableContactMethods { get; set; }

    }

    //public class CustomPasswordAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        string password = (string)value;



    //        return base.IsValid(value, validationContext);
    //    }
    //}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
