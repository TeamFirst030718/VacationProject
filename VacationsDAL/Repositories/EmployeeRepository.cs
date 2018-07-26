using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

        public Employee[] Get(Func<Employee,bool> whereCondition = null)
        {
            IQueryable<Employee> baseCondition = _context.Employees.Include(x=> x.Teams);
            return whereCondition == null ?
                baseCondition.ToArray() :
                baseCondition.Where(whereCondition).ToArray();
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
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }       
            catch (DbUpdateException e)
            {
            }
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
