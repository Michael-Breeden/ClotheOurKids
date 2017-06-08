using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClotheOurKids.Model.Repository
{
    public class RegisterFormRepository : IRegisterFormRepository
    {
        private ClotheOurKidsEntities context;

        public RegisterFormRepository()
        {
            context = new ClotheOurKidsEntities();
        }

        public IList<Office> GetAllOffices()
        {
            var query = from offices in context.Offices
                        select offices;
            var content = query.ToList<Office>();
            return content;
        }

        public IList<Position> GetPositionsByOffice(int officeId)
        {
            var officeTypeId = (from offices in context.Offices
                               where offices.OfficeId == officeId
                               select offices.OfficeTypeId).FirstOrDefault();

            var query = from positions in context.Positions
                        where positions.PositionOfficeTypes.Any(p => p.OfficeTypeId == officeTypeId)
                        select positions;

            var content = query.ToList<Position>();
            return content;
        }

        public IList<ContactMethod> GetAllContactMethods()
        {
            var query = from contactMethods in context.ContactMethods
                        select contactMethods;
            var content = query.ToList<ContactMethod>();
            return content;
        }

    }
}
