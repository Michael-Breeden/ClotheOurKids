using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClotheOurKids.Model.ViewModel;

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

        public IList<RegisterViewModel> GetAllOfficesForRegister()
        {
            var query = from offices in context.Offices
                        select new RegisterViewModel
                        {
                            OfficeId = offices.OfficeId,
                            //OfficeName = offices.Name.Length >= 25 ? offices.Name.Substring(0, 24) + "..." : offices.Name,
                            OfficeName = offices.Name,
                            PostalCode = offices.Address.PostalCode,
                            Location = offices.Address.City + ", " + offices.Address.State.Name
                        };


            var content = query.OrderBy(o => o.OfficeName).ToList<RegisterViewModel>();
            return content;
        }

        public IList<Office> GetOfficesByPostalCode(string postalCode)
        {
            var query = from offices in context.Offices
                        where offices.Address.PostalCode == postalCode
                        select offices;

            var content = query.OrderBy(office => office.Name).ToList<Office>();
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
