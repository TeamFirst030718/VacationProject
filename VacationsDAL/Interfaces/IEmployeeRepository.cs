using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        
        Employee GetByEmail(string email);
        void Update(Employee employee);
    }
}
