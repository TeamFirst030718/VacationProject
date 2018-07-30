using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class ValidateService : IValidateService
    {
        IEmployeeRepository _employees;

        public ValidateService(IEmployeeRepository employees)
        {
            _employees = employees;
        }

        public bool CheckEmailForExisting(string email)
        {
            return _employees.GetByEmail(email) == null ? false : true;
        }

        public bool CheckEmailOwner(string email, string id)
        {
            return _employees.GetByEmail(email).EmployeeID.Equals(id) ? false : true;
        }
    }
}
