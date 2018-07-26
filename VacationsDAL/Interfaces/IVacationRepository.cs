using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IVacationRepository : IRepository<Vacation>
    {
        void Update(Vacation employee);
    }
}
