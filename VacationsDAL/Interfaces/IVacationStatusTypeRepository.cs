using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IVacationStatusTypeRepository : IRepository<VacationStatusType>
    {
        VacationStatusType GetByType(string type);
    }
}
