using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClotheOurKids.Model.ViewModel;

namespace ClotheOurKids.Model.Repository
{
    public interface IRegisterFormRepository
    {
        IList<OfficeType> GetAllOfficeTypes();
        IList<Position> GetAllPositions();
        IList<Office> GetAllOffices();
        IList<RegisterViewModel> GetAllOfficesForRegister();
        IList<Office> GetOfficesByOfficeType(int officeTypeId);
        IList<Office> GetOfficesByPostalCode(string postalCode);
        IList<Position> GetPositionsByOffice(int officeId);
        IList<Position> GetPositionsByOfficeType(int officeTypeId);
        IList<ContactMethod> GetAllContactMethods();
    }
}
