using System;
using System.Linq;
using System.Web.Mvc;
using ClotheOurKids.Web.Models.ViewModel;
using ClotheOurKids.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace ClotheOurKids.Web.Controllers
{
    public class RequestClothesController : Controller
    {

        private IRequestFormRepository _repository;

        public RequestClothesController() : this(new RequestFormRepository())
        {

        }

        public RequestClothesController(IRequestFormRepository repository)
        {
            _repository = repository;
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

        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult GetOfficesByPositionId(string positionId)
        //{
        //    if (String.IsNullOrEmpty)
        //}

    }
}