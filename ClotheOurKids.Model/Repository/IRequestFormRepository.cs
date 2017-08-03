using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClotheOurKids.Model;

namespace ClotheOurKids.Model
{
    public interface IRequestFormRepository : IDisposable
    {
        IList<Grade> GetAllGrades();
        IList<Gender> GetGenders();
        IList<SchoolDistrict> GetSchoolDistricts();
        string GetOfficeType(string userId);
        IList<School> GetSchools();
        IList<School> GetSchoolByUser(string userId);
        IList<Urgency> GetUrgencies();
        IList<AgeGroup> GetShirtAgeGroups(string GenderId);
        IList<AgeGroup> GetPantAgeGroups(string GenderId);
        IList<ShirtSize> GetShirtSizes(byte AgeGroupId);
        IList<PantSize> GetPantSizes(byte AgeGroupId);
        System.DateTime GetEstimatedDeliveryDate(byte UrgencyId);
        AspNetUser GetUserByAppUserId(string userId);
        void InsertRequest(Request request);
        void Save();
    }
}
