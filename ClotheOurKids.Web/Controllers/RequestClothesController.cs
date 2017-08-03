﻿using System;
using System.Linq;
using System.Web.Mvc;
using ClotheOurKids.Web.Models.ViewModel;
using ClotheOurKids.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using ClotheOurKids.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClotheOurKids.Web.Controllers
{
    public class RequestClothesController : Controller
    {        
        private IRequestFormRepository _repository;
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public RequestClothesController()
        {
            this._repository = new RequestFormRepository(new ClotheOurKidsEntities());
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public RequestClothesController(IRequestFormRepository repository)
        {
            this._repository = repository;
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
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
        public ActionResult RequestClothesForm(RequestFormViewModel model)
        {
            if (ModelState.IsValid)
            {

                bool NeedShirt = Convert.ToBoolean(Request.Form["NeedShirt"]);
                bool NeedPant = Convert.ToBoolean(Request.Form["NeedPant"]);
                bool NeedUnderwear = Convert.ToBoolean(Request.Form["NeedUnderwear"]);
                bool NeedSocks = Convert.ToBoolean(Request.Form["NeedSocks"]);
                bool NeedShoes = Convert.ToBoolean(Request.Form["NeedShoes"]);
                bool NeedCoat = Convert.ToBoolean(Request.Form["NeedCoat"]);
                bool NeedHygiene = Convert.ToBoolean(Request.Form["NeedHygiene"]);

                //Need validation based on Checkboxes

                string userId = User.Identity.GetUserId();
                //var user = UserManager.FindById(userId);

                var aspNetUser = _repository.GetUserByAppUserId(userId);

                string school = aspNetUser.Office.Schools.SingleOrDefault().ToString();
                short schoolId = 0;
                bool isValidSchool = short.TryParse(school, out schoolId);

                var neededItems = new NeededItem
                {
                    ShirtFlag = NeedShirt,
                    PantFlag = NeedPant,
                    UnderwearFlag = NeedUnderwear,
                    SockFlag = NeedSocks,
                    ShoeFlag = NeedShoes,
                    CoatFlag = NeedCoat,
                    HygieneFlag = NeedHygiene
                };

                var request = new Request
                {
                    GenderId = model.GenderId,
                    GradeId = model.GradeId,
                    UrgencyId = model.UrgencyId,
                    ShirtSizeId = model.ShirtSizeId,
                    PantSizeId = model.PantSizeId,
                    UnderwearSize = model.UnderwearSize,
                    ShoeSize = model.ShoeSize,
                    Comments = model.Comments,
                    SchoolId = schoolId == 0 ? (model.SchoolId == 0 ? null : model.SchoolId) : schoolId,
                    DateRequested = DateTime.Today,
                    DateEstimatedDelivery = _repository.GetEstimatedDeliveryDate(model.UrgencyId),
                    SubmittedByUserId = userId,
                    RequestStatusId = 1,
                    NeededItem = neededItems
                    //Need PantLengthSize                    
                };

                _repository.InsertRequest(request);
                _repository.Save();


                return RedirectToAction("Index", "Home");
            }

            PopulateRequestFormModel(model);

            //If we got this far, something failed, redisplay form
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