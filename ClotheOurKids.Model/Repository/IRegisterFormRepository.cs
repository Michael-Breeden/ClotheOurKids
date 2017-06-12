using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model.Repository
{
    public interface IRegisterFormRepository
    {
        IList<Position> GetAllPositions();
        IList<Office> GetAllOffices();
        IList<Position> GetPositionsByOffice(int officeId);
        IList<ContactMethod> GetAllContactMethods();
    }
}
