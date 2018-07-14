using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;


namespace VacationsDAL.Repositories
{
    public class EmployeesRepository : IRepository<Employee>
    {
        private readonly VacationsContext _context;
            
        public EmployeesRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(string id)
        {
            return _context.Employees.FirstOrDefault(x => x.EmployeeID == id);
        }

        public void Remove(string id)
        {
            var obj = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);

            if (obj != null)
            {
                _context.Employees.Remove(obj);
            }
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
        }

    }
}
