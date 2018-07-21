﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;

namespace VacationsDAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetByEmail(string email);
        void SaveChanges();
    }
}
