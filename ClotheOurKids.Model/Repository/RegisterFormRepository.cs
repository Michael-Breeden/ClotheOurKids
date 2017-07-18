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

        public IList<OfficeType> GetAllOfficeTypes()
        {
            var query = from officeTypes in context.OfficeTypes
                        select officeTypes;
            var content = query.ToList<OfficeType>();
            return content;
        }        

        public IList<Office> GetAllOffices()
        {
            var query = from offices in context.Offices
                        select offices;
            var content = query.OrderBy(office => office.Name).ToList<Office>();
            return content;
        }

        public IList<Position> GetAllPositions()
        {
            var query = from positions in context.Positions
                        select positions;
            var content = query.ToList<Position>();
            return content;
        }

        public IList<Position> GetPositionsByOfficeType(int officeTypeId)
        {
            var query = from positions in context.Positions
                        where positions.PositionOfficeTypes.Any(p => p.OfficeTypeId == officeTypeId)
                        select positions;
            var content = query.ToList<Position>();
            return content;
        }

        public IList<Office> GetOfficesByOfficeType(int officeTypeId)
        {
            var query = from offices in context.Offices
                        where offices.OfficeTypeId == officeTypeId
                        select offices;

            var content = query.OrderBy(office => office.Name).ToList<Office>();
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
