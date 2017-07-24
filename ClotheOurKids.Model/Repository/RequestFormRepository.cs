using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClotheOurKids.Model;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestFormRepository : IRequestFormRepository
    {
        
        private ClotheOurKidsEntities context;

        public RequestFormRepository()
        {
            context = new ClotheOurKidsEntities();
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

        public IList<SchoolDistrict> GetSchoolDistricts()
        {
            var query = from schoolDistricts in context.SchoolDistricts
                        select schoolDistricts;
            var content = query.ToList<SchoolDistrict>();
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

        public IList<Urgency> GetUrgencies()
        {
            var query = from urgency in context.Urgencies
                        select urgency;
            var content = query.ToList<Urgency>();
            return content;
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

    }
}