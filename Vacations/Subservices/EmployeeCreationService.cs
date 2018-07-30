﻿using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace Vacations.Subservice
{
    public static class EmployeeCreationService
    {
        public static bool CreateAndRegisterEmployee(EmployeeViewModel model, string role, ApplicationUserManager userManager, ApplicationUser user, IEmployeeService employeeService)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = userManager.Create(user, "123asdQ!");

                if (result.Succeeded)
                {
                    userManager.AddToRole(model.EmployeeID, role);

                    var _employee = Mapper.Map<EmployeeViewModel, EmployeeDTO>(model);

                    employeeService.Create(_employee);

                    transaction.Complete();

                    return result.Succeeded;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}