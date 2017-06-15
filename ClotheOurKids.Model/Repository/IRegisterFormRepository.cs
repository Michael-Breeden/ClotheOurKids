using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model.Repository
{
    public interface IRegisterFormRepository
    {
        IList<OfficeType> GetAllOfficeTypes();
        IList<Position> GetAllPositions();
        IList<Office> GetAllOffices();
        IList<Office> GetOfficesByOfficeType(int officeTypeId);
        IList<Position> GetPositionsByOffice(int officeId);
        IList<Position> GetPositionsByOfficeType(int officeTypeId);
        IList<ContactMethod> GetAllContactMethods();
    }
}
