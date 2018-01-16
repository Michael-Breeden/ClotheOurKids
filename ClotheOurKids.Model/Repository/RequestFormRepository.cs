using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClotheOurKids.Model;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestFormRepository : IRequestFormRepository, IDisposable
    {
        
        private ClotheOurKidsEntities context;        

        public RequestFormRepository(ClotheOurKidsEntities context)
        {
            this.context = context;
        }
        

        public IList<Grade> GetAllGrades()
        {
            var query = from grades in context.Grades
                        select grades;
            var content = query.ToList<Grade>();
            return content;
        }

        public IList<Gender> GetGenders()
        {
            var query = from genders in context.Genders
                        select genders;
            var content = query.ToList<Gender>();
            return content;
        }

        public string  GetOfficeType(string userId)
        {
            var officeTypeId = (from users in context.AspNetUsers
                                where users.Id == userId
                                select users.Office.OfficeType.Name).SingleOrDefault();
            return officeTypeId;
        }

        public IList<SchoolDistrict> GetSchoolDistrictsByUser(string userId)
        {
            var schoolDistrictId = (from schools in context.Schools
                          where schools.Office.AspNetUsers.Any(u => u.Id == userId)
                          select schools.SchoolDistrictId).SingleOrDefault();

            IQueryable<SchoolDistrict> schoolDistrict;

            if (schoolDistrictId != 0)
            {
                schoolDistrict = from schoolDistricts in context.SchoolDistricts
                        where schoolDistricts.SchoolDistrictId == schoolDistrictId
                        select schoolDistricts;
            }
            else
            {
                schoolDistrict = from schoolDistricts in context.SchoolDistricts
                                 select schoolDistricts;
            }

            var content = schoolDistrict.OrderBy(s => s.Name).ToList<SchoolDistrict>();

            return content;
        }

        public IList<School> GetSchools()
        {
            var query = from schools in context.Schools
                        select schools;
            var content = query.ToList<School>();
            return content;
        }



        public IList<School> GetSchoolByUser(string userId)
        {
            var officeId = (from users in context.AspNetUsers
                           where users.Id == userId && users.Office.OfficeType.Name == "School"
                           select users.OfficeId).SingleOrDefault();

            IQueryable<School> query;

            if (officeId != 0)
            {
                query = from schools in context.Schools
                        where schools.OfficeId == officeId
                        select schools;
            }
            else
            {
                query = from schools in context.Schools
                        select schools;
            }            

            var content = query.OrderBy(school => school.Office.Name).ToList<School>();

            return content;

        }

        public IList<School> GetSchoolBySchoolDistrict(short schoolDistrictId)
        {

            IQueryable<School> query;

            if (schoolDistrictId != 0)
            {
                query = from schools in context.Schools
                        where schools.SchoolDistrictId == schoolDistrictId
                        select schools;
            }
            else
            {
                query = from schools in context.Schools
                        select schools;
            }

            var content = query.OrderBy(school => school.Office.Name).ToList<School>();

            return content;

        }

        public IList<Urgency> GetUrgencies()
        {
            var query = from urgency in context.Urgencies
                        select urgency;
            var content = query.ToList<Urgency>();
            return content;
        }

        public System.DateTime GetEstimatedDeliveryDate(byte UrgencyId)
        {
            byte deliveryDays = (from urgency in context.Urgencies
                        where urgency.UrgencyId == UrgencyId
                        select urgency.DaysForDelivery).SingleOrDefault();

            var deliveryDate = DateTime.Today.AddDays(deliveryDays);

            return deliveryDate;

        }

        public IList<AgeGroup> GetShirtAgeGroups(string GenderId)
        {
            var query = (from shirtSizes in context.ShirtSizes
                        where shirtSizes.GenderId == GenderId
                        select shirtSizes.AgeGroup).Distinct();
            var ageGroups = query.ToList<AgeGroup>();
            return ageGroups;
        }

        public IList<AgeGroup> GetPantAgeGroups(string GenderId)
        {
            var query = (from pantSizes in context.PantSizes
                        where pantSizes.GenderId == GenderId
                        select pantSizes.AgeGroup).Distinct();
            var ageGroups = query.ToList<AgeGroup>();
            return ageGroups;
        }

        public IList<ShirtSize> GetShirtSizes (byte AgeGroupId)
        {
            var query = from shirtSizes in context.ShirtSizes
                        where shirtSizes.AgeGroupId == AgeGroupId
                        select shirtSizes;

            var content = query.ToList<ShirtSize>();
            return content;
        }

        public IList<PantSize> GetPantSizes(byte AgeGroupId)
        {
            var query = from pantSizes in context.PantSizes
                        where pantSizes.AgeGroupId == AgeGroupId
                        select pantSizes;

            var content = query.ToList<PantSize>();
            return content;
        }


        public IList<Request> GetRequestsByUser(string userId)
        {
            var query = from requests in context.Requests
                        where requests.AspNetUser.Id == userId
                        select requests;

            var content = query.ToList<Request>();
            return content;
        }

        public IList<Request> GetRequests()
        {
            var query = from requests in context.Requests
                        select requests;

            var content = query.ToList<Request>();
            return content;
        }

        public IList<Request> GetRequestsByIdList(int[] ids)
        {
            var query = from requests in context.Requests
                        where ids.Contains(requests.RequestId)
                        select requests;

            var content = query.ToList<Request>();
            return content;
        }


        public AspNetUser GetUserByAppUserId(string userId)
        {
            var query = from user in context.AspNetUsers
                        where user.Id == userId
                        select user;
            var content = query.SingleOrDefault();
            return content;
        }

        public string GetPantSizeNameById(int pantSizeId)
        {
            string size = (from pantSizes in context.PantSizes
                           where pantSizes.PantSizeId == pantSizeId
                           select pantSizes.Name).SingleOrDefault();
            return size;
        }

        public Request AttachRequest(Request request)
        {
            context.Entry(request).Reference(s => s.School).Load();
            context.Entry(request).Reference(g => g.Grade).Load();
            context.Entry(request).Reference(u => u.Urgency).Load();
            context.Entry(request).Reference(s => s.ShirtSize).Load();
            context.Entry(request).Reference(p => p.PantSize1).Load();

            return request;
        }

        public void InsertRequest (Request request)
        {
            context.Requests.Add(request);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}