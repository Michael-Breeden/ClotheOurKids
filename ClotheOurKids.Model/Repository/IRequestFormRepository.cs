using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClotheOurKids.Model.DAL;

namespace ClotheOurKids.Model
{
    public interface IRequestFormRepository
    {
        IList<Position> GetAllPositions();
        IList<Position> GetPositionsByOfficeId(int officeTypeId);
        IList<Office> GetAllOffices();
        IList<Office> GetOfficesByPositionId(int positionId);
        IList<Grade> GetAllGrades();
        //IList<AgeGroup> GetAllAgeGroups();
        //IList<Size> GetSizesByClothesType(string clothesType);
        //IList<Size> GetSizesByGenderAndClothesType(string gender, string clothesType);


    }
}
