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

        ITeamRepository _teams;

        public ValidateService(IEmployeeRepository employees, ITeamRepository teams)
        {
            _employees = employees;
            _teams = teams;
        }

        public bool CheckEmail(string email)
        {
            return _employees.GetByEmail(email) == null ? false : true;
        }

        public bool CheckEmailOwner(string email, string id)
        {
            return _employees.GetByEmail(email).EmployeeID.Equals(id) ? false : true;
        }

        public bool CheckTeamName(string teamName)
        {
            return _teams.GetByName(teamName) == null ? false : true;
        }

        public bool CheckTeam(string teamName, string id)
        {
            return _teams.GetByName(teamName).TeamID.Equals(id) ? false : true;
        }
    }
}
