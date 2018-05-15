using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClotheOurKids.Web.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Helpers;
using ClotheOurKids.Model;
using ClotheOurKids.Model.Repository;
using System.Configuration;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace ClotheOurKids.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Utility
        // Add RoleManager
        #region public ApplicationRoleManager RoleManager
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        #endregion

        // Add CreateAdminIfNeeded
        #region private void CreateAdminIfNeeded()
        private void CreateAdminRoleIfNeeded()
        {
            // Get Admin Account
            string AdminUserName = "givekidsclothes@gmail.com";
            // See if Admin exists
            var objAdminUser = UserManager.FindByEmail(AdminUserName);
            if (objAdminUser != null)
            {
                //See if the Admin role exists
                if (!RoleManager.RoleExists("Administrator"))
                {
                    // Create the Admin Role (if needed)
                    IdentityRole objAdminRole = new IdentityRole("Administrator");
                    RoleManager.Create(objAdminRole);
                }
                 // Put user in Admin role
                UserManager.AddToRole(objAdminUser.Id, "Administrator");
            }
        }
        #endregion

        //
        // GET: /Login
        [AllowAnonymous]
        [Route("Login", Name = "LoginPage")]
        public ActionResult Login(string returnUrl)
        {
            

            return View();
        }

        //
        // POST: /Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            string totalError;


            if (!ModelState.IsValid)
            {
                //return PartialView("_LoginForm", model); 
                totalError = CompileErrorMsg(ModelState);

                return Json(new { Success = 0, errorMsg = new Exception(totalError).Message.ToString() });
            }

            var user = await UserManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                if (await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    await SignInManager.SignInAsync(user, model.RememberMe, model.RememberMe);

                    return Json(new { Success = 1, errorMsg = "" });
                }
                else
                {
                    //var callbackUrl = Url.Action("Confirm", "Account", new { Email = user.Email}, protocol: Request.Url.Scheme);
                    var tempAppSetting = ConfigurationManager.AppSettings["SMTPHost"];
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Clothe Our Kids Account Confirmation");
                    return Json(new { Success = 0, errorMsg = "You must have a confirmed email to log on. <br/> The confirmation token has been resent to your email address."});
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }


            totalError = CompileErrorMsg(ModelState);

            ////If we got this far, something failed, redisplay form
            return Json(new { Success = 0, errorMsg = new Exception(totalError).Message.ToString(), confirmEmail = "" });



            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        //return PartialView("_LoginForm", model); //
            //        return View(model);
            //}
        }


        private string CompileErrorMsg (ModelStateDictionary ModelState)
        {
            string totalError = "";
            foreach (var obj in ModelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        totalError = totalError + error.ErrorMessage + Environment.NewLine;
                    }
                }
            }

            return totalError;
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        [Route("Register", Name = "RegisterPage")]
        public ActionResult Register()
        {
            //CreateAdminRoleIfNeeded();

            var model = new RegisterViewModel();
                
            PopulateRegisterModel(model);

            return View(model);
        }


        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Get-All-Offices", Name = "GetAllOffices")]
        public ActionResult GetOffices()
        {

            var repository = new RegisterFormRepository();
            var offices = repository.GetAllOfficesForRegister();        

            return Json(new { data = offices }, JsonRequestBehavior.AllowGet);
        }

        //[AllowAnonymous]
        //[AcceptVerbs(HttpVerbs.Get)]
        //[Route("Get-Offices", Name = "GetOffices")]
        //public ActionResult GetOfficesByPostalCode (string postalCode)
        //{
        //    if (String.IsNullOrEmpty(postalCode))
        //    {
        //        throw new ArgumentNullException("postalCode");
        //    }

        //    var repository = new RegisterFormRepository();
        //    var offices = repository.GetOfficesByPostalCode(postalCode);

        //    var result = (from o in offices
        //                  select new
        //                  {
        //                      id = o.OfficeId,
        //                      name = o.Name
        //                  }).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Get-Positions", Name = "GetPositions")]
        public ActionResult GetPositionsByOfficeId (string officeId)
        {
            if (String.IsNullOrEmpty(officeId))
            {
                throw new ArgumentNullException("officeId");
            }

            int id = 0;
            var repository = new RegisterFormRepository();
            bool isValid = Int32.TryParse(officeId, out id);
            var positions = repository.GetPositionsByOffice(id);

            var result = (from p in positions
                          select new
                          {
                              id = p.PositionId,
                              name = p.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private RegisterViewModel PopulateRegisterModel (RegisterViewModel model)
        {
            var repository = new RegisterFormRepository();
            
            //Populate Office Types
            //var officeTypes = repository.GetAllOfficeTypes();
            //var officeTypeList = (from ot in officeTypes
            //                      select new
            //                      {
            //                          id = ot.OfficeTypeId,
            //                          name = ot.Name
            //                      }).ToList();

            //foreach (var officeType in officeTypeList)
            //{
            //    model.AvailableOfficeTypes.Add(new SelectListItem()
            //    {
            //        Text = officeType.name,
            //        Value = officeType.id.ToString()
            //    });

            //}


            //Populate Contact Methods
            var contactMethods = repository.GetAllContactMethods();
            var contactMethodList = (from c in contactMethods
                                     select new
                                     {
                                         id = c.ContactMethodId,
                                         name = c.Name
                                     }).ToList();

            foreach (var contactMethod in contactMethodList)
            {
                model.AvailableContactMethods.Add(new SelectListItem()
                {
                    Text = contactMethod.name,
                    Value = contactMethod.id.ToString()
                });

            }

            //Seed Position and Office Dropdowns with placeholder
            model.AvailablePositions.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            if (model.OfficeId.GetValueOrDefault(0) != 0)
            {
                var positions = repository.GetPositionsByOffice((int)model.OfficeId);
                var positionList = (from p in positions
                                    select new
                                    {
                                        id = p.PositionId,
                                        name = p.Name
                                    }).OrderBy(p => p.name).ToList();

                foreach (var position in positionList)
                {
                    model.AvailablePositions.Add(new SelectListItem()
                    {
                        Text = position.name,
                        Value = position.id.ToString()
                    });
                }
            }

            //Populate Office and Position dropdowns based on OfficeTypeId
            //if (model.OfficeTypeId.HasValue)
            //{
            //    var offices = repository.GetOfficesByOfficeType((int)model.OfficeTypeId);
            //    var officeList = (from o in offices
            //                      select new
            //                      {
            //                          id = o.OfficeId,
            //                          name = o.Name
            //                      }).ToList();

            //    foreach (var office in officeList)
            //    {
            //        model.AvailableOffices.Add(new SelectListItem()
            //        {
            //            Text = office.name,
            //            Value = office.id.ToString()
            //        });
            //    }


            //    var positions = repository.GetPositionsByOfficeType((int)model.OfficeTypeId);
            //    var positionList = (from p in positions
            //                        select new
            //                        {
            //                            id = p.PositionId,
            //                            name = p.Name
            //                        }).ToList();

            //    foreach (var position in positionList)
            //    {
            //        model.AvailablePositions.Add(new SelectListItem()
            //        {
            //            Text = position.name,
            //            Value = position.id.ToString()
            //        });
            //    }

            //}

            return model;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Register", Name = "RegisterPost")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PositionId = (short)model.PositionId, OfficeId = (short)model.OfficeId, PhoneNumber = model.PhoneNumber, ContactMethodId = (short)model.ContactMethodId };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Commented to prevent login until user confirms email address
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);


                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Clothe Our Kids Account Confirmation");


                    // Uncomment to debug locally 
                    // TempData["ViewBagLink"] = callbackUrl;

                  
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
                AddErrors(result);
            }

            PopulateRegisterModel(model);

            TempData["SubmitError"] = "Error Submitting!";

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Confirm (string Email)
        {
            ViewBag.Email = Email;
            return View();
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link:
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userID, subject, "You must confirm your email before you can log in to GiveKidsClothes.com and request clothes from Clothe Our Kids. Please confirm your account by clicking <a href=\"" + callbackUrl + "\"> here</a>");


            return callbackUrl;
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        [Route("ForgotPassword", Name = "ForgotPasswordPage")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ForgotPassword", Name = "ForgotPasswordPost")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "We receieved a request to reset your password for GiveKidsClothes.com. <br/> <br/> If you did not request this reset, please let us know at 256-887-KIDS(5437). <br/> <br/> Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PositionId = model.PositionId,
                    OfficeId = model.OfficeId
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}