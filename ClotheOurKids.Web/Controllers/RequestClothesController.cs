using System;
using System.Linq;
using System.Web.Mvc;
using ClotheOurKids.Web.Models.ViewModel;

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



        [Route("Request-Clothes/Form", Name = "RequestClothesForm")]
        public ActionResult RequestClothesForm()
        {
            return View();
            //RequestFormModel model = new RequestFormModel();

            //model.AvailablePositions.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});

            //model.AvailableOffices.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});

            //model.AvailableGenders.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});

            //model.AvailableGrades.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});

            //model.AvailableShirtSizes.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});

            //model.AvailablePantSizes.Add(new SelectListItem()
            //{
            //    Text = "",
            //    Value = ""
            //});



            //var positions = _repository.GetAllPositions();
            //foreach (var position in positions)
            //{
            //    model.AvailablePositions.Add(new SelectListItem()
            //    {
            //        Text = position.Name,
            //        Value = position.PositionId.ToString()
            //    });

            //}


            //var offices = _repository.GetAllOffices();
            //foreach (var office in offices)
            //{
            //    model.AvailableOffices.Add(new SelectListItem()
            //    {
            //        Text = office.Name,
            //        Value = office.OfficeId.ToString()
            //    });
            //}


            //model.AvailableGenders.Add(new SelectListItem()
            //{
            //    Text = "Male",
            //    Value = "Male"
            //});

            //model.AvailableGenders.Add(new SelectListItem()
            //{
            //    Text = "Female",
            //    Value = "Female"
            //});


            //var grades = _repository.GetAllGrades();
            //foreach (var grade in grades)
            //{
            //    model.AvailableGrades.Add(new SelectListItem()
            //    {
            //        Text = grade.Name,
            //        Value = grade.GradeId.ToString()
            //    });
            //}

            //var shirtSizes = _repository.GetSizesByClothesType("Shirt");
            //foreach (var size in shirtSizes)
            //{
            //    model.AvailableShirtSizes.Add(new SelectListItem()
            //    {
            //        Text = size.Name,
            //        Value = size.SizeId.ToString()
            //    });
            //}

            //var pantSizes = _repository.GetSizesByClothesType("Pant");
            //foreach (var size in pantSizes)
            //{
            //    model.AvailablePantSizes.Add(new SelectListItem()
            //    {
            //        Text = size.Name,
            //        Value = size.SizeId.ToString()
            //    });
            //}

            //return View(model);
        }



        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult GetOfficesByPositionId(string positionId)
        //{
        //    if (String.IsNullOrEmpty)
        //}

    }
}