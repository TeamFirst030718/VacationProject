using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Transactions;
using Vacations.Models;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace Vacations.Subservices
{
    public static class EmployeeEditService
    {
        public static void EditEmployee(EmployeeViewModel model, string role, ApplicationUserManager userManager, ApplicationUser user, IEmployeeService employeeService, string id)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                user.Email = model.WorkEmail;
                userManager.Update(user);

                var employee = employeeService.GetUserById(id);

                if (employee.Status != model.Status)
                {
                    if (!model.Status)
                    {
                        model.DateOfDismissal = DateTime.Today;
                    }
                    else
                    {
                        model.DateOfDismissal = null;
                        model.HireDate = DateTime.Today;
                    }
                }

                var userRole = userManager.GetRoles(model.EmployeeID).First();

                if (userRole != role)
                {
                    userManager.RemoveFromRole(model.EmployeeID, userRole);
                    userManager.AddToRoles(model.EmployeeID, role);
                }

                employeeService.UpdateEmployee(Mapper.Map<EmployeeViewModel, EmployeeDTO>(model));

                transaction.Complete();
            }
        }
    }
}