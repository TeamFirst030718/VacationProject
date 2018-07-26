using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Update(Team team);
        IEnumerable<Team> GetAll();
    }
}
