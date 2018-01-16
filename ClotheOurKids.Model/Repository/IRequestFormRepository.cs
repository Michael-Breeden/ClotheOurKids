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
        IList<SchoolDistrict> GetSchoolDistrictsByUser(string userId);
        string GetOfficeType(string userId);
        IList<School> GetSchools();
        IList<School> GetSchoolByUser(string userId);
        IList<School> GetSchoolBySchoolDistrict(short schoolDistrictId);
        IList<Urgency> GetUrgencies();
        IList<AgeGroup> GetShirtAgeGroups(string GenderId);
        IList<AgeGroup> GetPantAgeGroups(string GenderId);
        IList<ShirtSize> GetShirtSizes(byte AgeGroupId);
        IList<PantSize> GetPantSizes(byte AgeGroupId);
        IList<Request> GetRequestsByUser(string userId);
        IList<Request> GetRequests();
        IList<Request> GetRequestsByIdList(int[] ids);
        System.DateTime GetEstimatedDeliveryDate(byte UrgencyId);
        AspNetUser GetUserByAppUserId(string userId);
        string GetPantSizeNameById(int pantSizeId);
        Request AttachRequest(Request request);
        void InsertRequest(Request request);
        void Save();
    }
}
