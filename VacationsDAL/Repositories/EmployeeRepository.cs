using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsDAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly VacationsContext _context; 
            
        public EmployeeRepository(VacationsContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Include(x=>x.Teams).ToList();
        }

        public Employee GetById(string id)
        {
            return _context.Employees.FirstOrDefault(x => x.EmployeeID == id);
        }

        public Employee GetByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(x => x.WorkEmail == email);
        }

        public void Remove(string id)
        {
            var obj = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);

            if (obj != null)
            {
                _context.Employees.Remove(obj);
                _context.SaveChanges();
            }
        }

        public void Add(Employee employee)
        {          
                _context.Employees.Add(employee);
                _context.SaveChanges();        
        }

        public void Update(Employee employee)
        {
                _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
