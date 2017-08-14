using System;
using System.Linq;
using System.Web.Mvc;
using ClotheOurKids.Web.Models.ViewModel;
using ClotheOurKids.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ClotheOurKids.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Postal;

namespace ClotheOurKids.Web.Controllers
{
    public class RequestClothesController : Controller
    {        
        private IRequestFormRepository _repository;
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        protected EmailService aspEmailService { get; set; }
        readonly IEmailService emailService;

        public RequestClothesController()
        {
            this._repository = new RequestFormRepository(new ClotheOurKidsEntities());
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            this.aspEmailService = new EmailService();
            this.emailService = new Postal.EmailService();
        }

        public RequestClothesController(IRequestFormRepository repository, IEmailService emailService)
        {
            this._repository = repository;
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            this.aspEmailService = new EmailService();
            this.emailService = emailService;
        }

        [Route("Request-Clothes", Name = "RequestClothes")]
        public ActionResult RequestClothes()
        {
            return View();
        }

        private RequestFormViewModel PopulateRequestFormModel (RequestFormViewModel model)
        {

            model.AvailableGrades.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailableGenders.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailableSchools.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailableSchools.Add(new SelectListItem()
            {
                Text = "Unknown",
                Value = "-1"
            });

            model.AvailableShirtAgeGroups.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailablePantAgeGroups.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailableShirtSizes.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailablePantSizes.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });

            model.AvailablePantLengthSizes.Add(new SelectListItem()
            {
                Text = "Select...",
                Value = "0"
            });



            var grades = _repository.GetAllGrades();
            var gradeList = (from g in grades
                             select new
                             {
                                 id = g.GradeId,
                                 name = g.Name
                             }).ToList();

            foreach (var grade in gradeList)
            {
                model.AvailableGrades.Add(new SelectListItem()
                {
                    Text = grade.name,
                    Value = grade.id.ToString()
                });
            }
           

            var genders = _repository.GetGenders();
            var genderList = (from g in genders
                              select new
                              {
                                  id = g.GenderId,
                                  name = g.GenderId
                              }).ToList();

            foreach (var gender in genderList)
            {
                model.AvailableGenders.Add(new SelectListItem()
                {
                    Text = gender.name,
                    Value = gender.id
                });
            }


            var userId = User.Identity.GetUserId();

            var officeType = _repository.GetOfficeType(userId);
            model.officeType = officeType;

            //var schoolDistricts = _repository.GetSchoolDistrictsByUser(userId);
            //var schoolDistrictList = (from s in schoolDistricts
            //                          select new
            //                          {
            //                              id = s.SchoolDistrictId,
            //                              name = s.Name
            //                          }).ToList();
            //foreach (var schoolDistrict in schoolDistrictList)
            //{
            //    model.AvailableSchoolDistricts.Add(new SelectListItem()
            //    {
            //        Text = schoolDistrict.name,
            //        Value = schoolDistrict.id.ToString()
            //    });
            //}

            var schools = _repository.GetSchoolByUser(userId);
            var schoolList = (from s in schools
                              select new
                              {
                                  id = s.SchoolId,
                                  name = s.Office.Name
                              }).ToList();
            foreach (var school in schoolList)
            {
                model.AvailableSchools.Add(new SelectListItem()
                {
                    Text = school.name,
                    Value = school.id.ToString()
                });
            }

            if (officeType == "School")
            {

                //Set to 0 and find school for this user during form submission
                //model.SchoolId = 0;


                //string userSchoolDistrict = model.AvailableSchoolDistricts.FirstOrDefault().Value;
                //short userSchoolDistrictShort;

                //short.TryParse(userSchoolDistrict, out userSchoolDistrictShort);
                //model.SchoolDistrictId = userSchoolDistrictShort;


                string userSchool = model.AvailableSchools.FirstOrDefault().Value;
                short userSchoolShort;

                short.TryParse(userSchool, out userSchoolShort);

                model.SchoolId = userSchoolShort;

            }

            var urgencies = _repository.GetUrgencies();
            var urgencyList = (from u in urgencies
                               select new
                               {
                                   id = u.UrgencyId,
                                   name = u.DaysForDelivery > 1 ? u.Name + " (" + u.DaysForDelivery + " days)" : u.Name + " (" + u.DaysForDelivery + "-3 days)"
                               }).ToList();

            foreach (var urgency in urgencyList)
            {
                model.AvailableUrgencies.Add(new SelectListItem()
                {
                    Text = urgency.name,
                    Value = urgency.id.ToString()
                });
            }


            if (model.GenderId != "0")
            {
                var shirtAgeGroups = _repository.GetShirtAgeGroups(model.GenderId);
                var shirtAgeGroupList = (from a in shirtAgeGroups
                                         select new
                                         {
                                             id = a.AgeGroupId,
                                             name = a.Name
                                         }).ToList();

                foreach (var ageGroup in shirtAgeGroupList)
                {
                    model.AvailableShirtAgeGroups.Add(new SelectListItem()
                    {
                        Text = ageGroup.name,
                        Value = ageGroup.id.ToString()
                    });
                }


                var pantAgeGroups = _repository.GetPantAgeGroups(model.GenderId);
                var pantAgeGroupList = (from p in pantAgeGroups
                                        select new
                                        {
                                            id = p.AgeGroupId,
                                            name = p.Name
                                        }).ToList();

                foreach (var pantGroup in pantAgeGroupList)
                {
                    model.AvailablePantAgeGroups.Add(new SelectListItem()
                    {
                        Text = pantGroup.name,
                        Value = pantGroup.id.ToString()
                    });
                }


            }

            //if (model.SchoolDistrictId.HasValue && officeType != "School")
            //{
            //    var schoolsUpdated = _repository.GetSchoolBySchoolDistrict((short)model.SchoolDistrictId);
            //    var schoolsUpdatedList = (from s in schoolsUpdated
            //                              select new
            //                              {
            //                                  id = s.SchoolId,
            //                                  name = s.Office.Name
            //                              }).ToList();

            //    model.AvailableSchools.Clear();

            //    foreach (var schoolUpdate in schoolsUpdatedList)
            //    {
            //        model.AvailableSchools.Add(new SelectListItem()
            //        {
            //            Text = schoolUpdate.name,
            //            Value = schoolUpdate.id.ToString()
            //        });
            //    }

            //}

            if (model.ShirtAgeGroupId.HasValue)
            {
                var shirtSizes = _repository.GetShirtSizes((byte)model.ShirtAgeGroupId);
                var shirtSizeList = (from s in shirtSizes
                                     select new
                                     {
                                         id = s.ShirtSizeId,
                                         name = s.Name
                                     }).ToList();

                foreach (var shirtSize in shirtSizeList)
                {
                    model.AvailableShirtSizes.Add(new SelectListItem()
                    {
                        Text = shirtSize.name,
                        Value = shirtSize.id.ToString()
                    });
                }
            }

            if (model.PantAgeGroupId.HasValue)
            {
                var pantSizes = _repository.GetPantSizes((byte)model.PantAgeGroupId);
                var pantSizeList = (from p in pantSizes
                                    select new
                                    {
                                        id = p.PantSizeId,
                                        name = p.Name
                                    }).ToList();

                foreach (var pantSize in pantSizeList)
                {
                    model.AvailablePantSizes.Add(new SelectListItem()
                    {
                        Text = pantSize.name,
                        Value = pantSize.id.ToString()
                    });

                    model.AvailablePantLengthSizes.Add(new SelectListItem()
                    {
                        Text = pantSize.name,
                        Value = pantSize.id.ToString()
                    });


                }
            }


            return model;
        }

        //GET
        [Authorize]
        [Route("Request-Clothes/Form", Name = "RequestClothesForm")]
        public ActionResult RequestClothesForm()
        {
            var model = new RequestFormViewModel();

            PopulateRequestFormModel(model);

            return View(model);

        }

        //POST
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("Request-Clothes/Form", Name = "RequestClothesFormPost")]
        public async Task<ActionResult> RequestClothesForm(RequestFormViewModel model)
        {

            string userId = User.Identity.GetUserId();
            //var user = UserManager.FindById(userId);

            var aspNetUser = _repository.GetUserByAppUserId(userId);

            string school = aspNetUser.Office.Schools.Select(s => s.SchoolId).SingleOrDefault().ToString();
            short schoolId = 0;
            bool isValidSchool = short.TryParse(school, out schoolId);

            if (isValidSchool && schoolId != 0)
            {
                ModelState.Remove("SchoolDistrictId");
                ModelState.Remove("SchoolId");
            }
            //Need validation based on Checkboxes
            if (model.NeedShirt)
            {
                if (model.ShirtSizeId == 0 || model.ShirtSizeId == null)
                {
                    ModelState.AddModelError("ShirtSizeId", "Please choose a size.");
                }
            }

            if (model.NeedPant)
            {
                if (model.PantSizeId == 0 || model.PantSizeId == null)
                {
                    ModelState.AddModelError("PantSizeId", "Please choose a size.");
                }
            }

            if (model.NeedUnderwear)
            {
                if (String.IsNullOrWhiteSpace(model.UnderwearSize))
                {
                    ModelState.AddModelError("UnderwearSize", "Please provide a size.");
                }
            }

            if (model.NeedSocks || model.NeedShoes)
            {
                if (String.IsNullOrWhiteSpace(model.ShoeSize))
                {
                    ModelState.AddModelError("ShoeSize", "Please provide a size.");
                }
            }

            //Check that at least one item is requested
            if (!model.NeedShirt && !model.NeedPant && !model.NeedUnderwear && !model.NeedSocks && !model.NeedShoes && !model.NeedCoat && !model.NeedHygiene)
            {
                ModelState.AddModelError(string.Empty, "You must select at least one item for us to give this child.");
            }

            if (ModelState.IsValid)
            {               
                var neededItems = new NeededItem
                {
                    ShirtFlag = model.NeedShirt,
                    PantFlag = model.NeedPant,
                    UnderwearFlag = model.NeedUnderwear,
                    SockFlag = model.NeedSocks,
                    ShoeFlag = model.NeedShoes,
                    CoatFlag = model.NeedCoat,
                    HygieneFlag = model.NeedHygiene
                };

                var request = new Request
                {
                    GenderId = model.GenderId,
                    GradeId = model.GradeId,
                    UrgencyId = model.UrgencyId,
                    ShirtSizeId = model.ShirtSizeId,
                    PantSizeId = model.PantSizeId,
                    UnderwearSize = string.IsNullOrEmpty(model.UnderwearSize) ? "" : model.UnderwearSize,
                    ShoeSize = string.IsNullOrEmpty(model.ShoeSize) ? "" : model.ShoeSize,
                    Comments = string.IsNullOrEmpty(model.Comments) ? "" : model.Comments,
                    SchoolId = schoolId == 0 ? (model.SchoolId <= 0 ? null : model.SchoolId) : schoolId,
                    DateRequested = DateTime.Now,
                    DateEstimatedDelivery = _repository.GetEstimatedDeliveryDate(model.UrgencyId),
                    SubmittedByUserId = userId,
                    RequestStatusId = 1,
                    NeededItem = neededItems,
                    //Add pant length if gender is Male and PantAgeGroup is 4 (Men)
                    PantLengthSizeId = (model.GenderId == "Male" && model.PantAgeGroupId == 4) ? model.PantLengthSizeId : null                
                };

                _repository.InsertRequest(request);
                _repository.Save();

                request = _repository.AttachRequest(request);

                string submitterName = request.AspNetUser.FirstName + " " + request.AspNetUser.LastName;
                string email = request.AspNetUser.Email;
                string phone = request.AspNetUser.PhoneNumber;
                string verified = request.AspNetUser.IsVerified.ToString();
                string office = request.AspNetUser.Office.Name;
                string position = request.AspNetUser.Position.Name;
                string contactMethod = request.AspNetUser.ContactMethod.Name;

                string needShirt = request.NeededItem.ShirtFlag ? "X" : "";
                string needPant = request.NeededItem.PantFlag ? "X" : "";
                string needUnderwear = request.NeededItem.UnderwearFlag ? "X" : "";
                string needSock = request.NeededItem.SockFlag ? "X" : "";
                string needShoe = request.NeededItem.ShoeFlag ? "X" : "";
                string needCoat = request.NeededItem.CoatFlag ? "X" : "";
                string needHygiene = request.NeededItem.HygieneFlag ? "X" : "";

                //string submittedSchoolDistrict = request.School.SchoolDistrict.Name;
                string submittedSchool = request.School != null ? request.School.Office.Name : "Unknown";
                string gender = request.GenderId;
                string grade = request.Grade.Name;
                string urgency = request.Urgency.Name + "(" + request.Urgency.DaysForDelivery + " days)";
                string shirt = request.ShirtSize != null ? request.ShirtSize.AgeGroup.Name + " - " + request.ShirtSize.Name : "";
                string pant = request.PantSize1 != null ? request.PantSize1.AgeGroup.Name + " - " + request.PantSize1.Name : "";
                string pantLength = request.PantLengthSizeId != null ? _repository.GetPantSizeNameById((int)request.PantLengthSizeId) : "";
                string underwear = request.UnderwearSize;
                string shoe = request.ShoeSize;
                string comments = request.Comments;



                //Send email to notify of request submission
                IdentityMessage message = new IdentityMessage();
                message.Destination = "givekidsclothes@gmail.com";
                message.Subject = "Request Number " + request.RequestId + " has been submitted by " + request.AspNetUser.FirstName + " " + request.AspNetUser.LastName;
                message.Body = @"
                    <html>
                        <body>
                          <h1>Request Number " + request.RequestId + @" </h1>
                          <div style=""margin-top: 20px;"">
                               <h2>Requestor</h2>

                               <table>   
                                   <tr style = ""text-align: center;"">   
                                     <th>Name</th>   
                                     <th>Email Address</th>      
                                     <th>Phone Number</th>
                                     <th>Verified</th>         
                                  </tr>
         
                                  <tr style = ""text-align: center;"">         
                                    <td> " + submitterName + @" </td>         
                                    <td> " + email + @" </td>         
                                    <td> " + phone + @" </td>         
                                    <td> " + verified + @" </td>         
                                  </tr>
         
                                </table>         
                         </div>
                        
                        <br/>

                        <div style = ""margin-top: 20px;"">          
                            <table>          
                            <tr style = ""text-align: center;"">          
                                <th> Office </th>          
                                <th> Position </th>          
                                <th> Contact Method </th>             
                            </tr>
             
                            <tr style = ""text-align: center;"">
             
                                <td> " + office + @" </td>             
                                <td> " + position + @" </td>             
                                <td> " + contactMethod + @" </td>             
                            </tr>
             
                            </table>
             
                        </div>
             

                        <div  style = ""margin-top: 40px;"">
              
                            <h2> Needed Items </h2> 
                                            
                            <table>                 
                                    <tr>                 
                                    <th>Shirts</th>                 
                                    <th>Pants</th>                 
                                    <th>Underwear</th>                 
                                    <th>Socks</th>                 
                                    <th>Shoes</th>                 
                                    <th>Coat</th>                 
                                    <th>Hygiene Kit</th>                    
                                </tr>
                    
                                <tr style = ""text-align: center;"">                     
                                    <td> " + needShirt + @" </td>                     
                                    <td> " + needPant + @" </td>                     
                                    <td> " + needUnderwear + @" </td>                     
                                    <td> " + needSock + @" </td>                     
                                    <td> " + needShoe + @" </td>                     
                                    <td> " + needCoat + @" </td>                     
                                    <td> " + needHygiene + @" </td>                     
                                </tr>
                     
                            </table>                     
                        </div>
                     

                        <div style = ""margin-top: 40px;"">                      
                        <h2> Request Details </h2>
                        
                        <h5>" + submittedSchool + @"</h5>
                                        
                        <table>                         
                            <tr style = ""text-align: center;"">                         
                            <th>Gender</th>                         
                            <th>Grade</th>                         
                            <th>Urgency</th>                         
                            </tr>
                         
                            <tr style = ""text-align: center;"">                          
                            <td>" + gender + @"</td>                         
                            <td>" + grade + @"</td>                          
                            <td>" + urgency + @"</td>                          
                            </tr>                          
                        </table>                          
                        </div>   

                        <br/>

                        <div style = ""margin-top: 20px;"">                           
                        <table>                           
                            <tr style = ""text-align: center;"">                           
                            <th> Shirt </th>                           
                            <th> Pant </th>                           
                            <th> Pant Length </th>                              
                            <th> Underwear </th>                              
                            <th> Shoe </th>                              
                            </tr>
                              
                            <tr style = ""text-align: center;"">                               
                            <td> " + shirt + @" </td>                               
                            <td> " + pant + @" </td>                               
                            <td> " + pantLength + @" </td>                               
                            <td> " + underwear + @" </td>                               
                            <td> " + shoe + @" </td>                               
                            </tr>                               
                        </table>                               
                        </div>
                               

                        <div style = ""margin-top: 20px;"">                                
                        <h5>Comments</h5>                                
                        <p>" + comments + @"</p>                                
                        </div>     
                    </ body >
                </ html >";

                await aspEmailService.SendAsync(message);

                TempData["RequestSuccess"] = "You submitted the request successfully!";
                return RedirectToAction("RequestClothesForm");

            }

            PopulateRequestFormModel(model);

            //If we got this far, something failed, redisplay form
            TempData["SubmitError"] = "Error Submitting!";
            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Get-AgeGroups", Name = "GetAgeGroups")]
        public ActionResult GetAgeGroupsByGenderId(string genderId, string ageGroupType)
        {
            IList<AgeGroup> ageGroups;

            if (String.IsNullOrEmpty(genderId))
            {
                throw new ArgumentNullException("genderId");
            }

            if (genderId != "Male" && genderId != "Female")
            {
                throw new ArgumentException("genderId");
            }

            if (ageGroupType != "Shirt" && ageGroupType != "Pant")
            {
                throw new ArgumentException("ageGroupType");
            }

            if (ageGroupType == "Shirt")
            {
                ageGroups = _repository.GetShirtAgeGroups(genderId);
            }
            else
            {
                ageGroups = _repository.GetPantAgeGroups(genderId);
            }


            var result = (from a in ageGroups
                          select new
                          {
                              id = a.AgeGroupId,
                              name = a.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        //[AcceptVerbs(HttpVerbs.Get)]
        //[Route("Get-Schools", Name = "GetSchools")]
        //public ActionResult GetSchoolsBySchoolDistrict(string schoolDistrictId)
        //{
        //    if (String.IsNullOrEmpty(schoolDistrictId))
        //    {
        //        throw new ArgumentNullException("schoolDistrictId");
        //    }

        //    short id = 0;
        //    bool isValid = short.TryParse(schoolDistrictId, out id);

        //    var schools = _repository.GetSchoolBySchoolDistrict(id);

        //    var result = (from s in schools
        //                  select new
        //                  {
        //                      id = s.SchoolId,
        //                      name = s.Office.Name
        //                  }).ToList();

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Get-Shirt-Sizes", Name = "GetShirtSizes")]
        public ActionResult GetShirtSizesByAgeGroup(string ageGroupId)
        {
            if (String.IsNullOrEmpty(ageGroupId))
            {
                throw new ArgumentNullException("ageGroupId");
            }

            byte id = 0;
            bool isValid = Byte.TryParse(ageGroupId, out id);

            var sizes = _repository.GetShirtSizes(id);

            var result = (from s in sizes
                          orderby (s.SortOrder)
                          select new
                          {
                              id = s.ShirtSizeId,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Get-Pant-Sizes", Name = "GetPantSizes")]
        public ActionResult GetPantSizesByAgeGroup(string ageGroupId)
        {
            if (String.IsNullOrEmpty(ageGroupId))
            {
                throw new ArgumentNullException("ageGroupId");
            }

            byte id = 0;
            bool isValid = Byte.TryParse(ageGroupId, out id);

            var sizes = _repository.GetPantSizes(id);

            var result = (from s in sizes
                          orderby (s.SortOrder)
                          select new
                          {
                              id = s.PantSizeId,
                              name = s.Name
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}