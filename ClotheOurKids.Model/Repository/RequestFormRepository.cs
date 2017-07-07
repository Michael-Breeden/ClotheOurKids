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

        public IList<Urgency> GetUrgencies()
        {
            var query = from urgency in context.Urgencies
                        select urgency;
            var content = query.ToList<Urgency>();
            return content;
        }

        public IList<AgeGroup> GetShirtAgeGroups(string GenderId)
        {
            var query = from shirtSizes in context.ShirtSizes
                        where shirtSizes.GenderId == GenderId
                        select shirtSizes.AgeGroup;
            var ageGroups = query.ToList<AgeGroup>();
            return ageGroups;
        }

        public IList<AgeGroup> GetPantAgeGroups(string GenderId)
        {
            var query = from pantSizes in context.PantSizes
                        where pantSizes.GenderId == GenderId
                        select pantSizes.AgeGroup;
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