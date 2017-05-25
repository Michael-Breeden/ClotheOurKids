using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClotheOurKids.Model.DAL;

namespace ClotheOurKids.Web.Models.ViewModel
{
    public class RequestFormRepository : IRequestFormRepository
    {
        
        private ClotheOurKidsContext _dataContext;

        public RequestFormRepository()
        {
            _dataContext = new ClotheOurKidsContext();
        }

        public IList<Position> GetAllPositions()
        {
            var query = from positions in _dataContext.Positions
                        select positions;
            var content = query.ToList<Position>();
            return content;
        }

        public IList<Position> GetPositionsByOfficeId(int officeTypeId)
        {
            var query = from positions in _dataContext.Positions
                        where positions.PositionOfficeTypes.Any(o => o.OfficeTypeId == officeTypeId)
                        select positions;
            var content = query.ToList<Position>();
            return content;
        }

        public IList<Office> GetAllOffices()
        {
            var query = from offices in _dataContext.Offices
                        select offices;
            var content = query.ToList<Office>();
            return content;
        }

        public IList<Office> GetOfficesByPositionId(int positionId)
        {
            var query = from offices in _dataContext.Offices
                        where offices.OfficeType.PositionOfficeTypes.Any(p => p.PositionId == positionId)
                        select offices;
            var content = query.ToList<Office>();
            return content;
        }

        public IList<Grade> GetAllGrades()
        {
            var query = from grades in _dataContext.Grades
                        select grades;
            var content = query.ToList<Grade>();
            return content;
        }
        //public IList<AgeGroup> GetAllAgeGroups()
        //{
        //    var query = from ageGroups in _dataContext.AgeGroups
        //                select ageGroups;
        //    var content = query.ToList<AgeGroup>();
        //    return content;
        //}
        //public IList<Size> GetSizesByClothesType(string clothesType)
        //{
        //    var query = from sizes in _dataContext.Sizes
        //                where sizes.SizeCharts.Any(g => (g.ClothesType.Name == clothesType))
        //                select sizes;
        //    var content = query.ToList<Size>();
        //    return content;
        //}

        //public IList<Size> GetSizesByGenderAndClothesType(string gender, string clothesType)
        //{
        //    var query = from sizes in _dataContext.Sizes
        //                where sizes.SizeCharts.Any(g => (g.Gender == gender &&  g.ClothesType.Name == clothesType))
        //                select sizes;
        //    var content = query.ToList<Size>();
        //    return content;
        //}

    }
}