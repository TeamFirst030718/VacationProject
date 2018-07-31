using System;
using System.Linq;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Entities;

namespace BalanceResetWebJob
{
    public class Functions
    {
        public void UpdateBalance()
        {
            if (DateTime.Today.ToString("ddMM").Equals("0101"))
            {
                using (var context = new VacationsContext())
                {
                    var employees = context.Employees.Where(x => x.Status).ToList();

                    foreach (var employee in employees)
                    {
                        if (employee.VacationBalance < 0)
                        {
                            employee.VacationBalance += 28;
                        }
                        else
                        {
                            employee.VacationBalance = 28;
                        }
                        /*employee.Transactions.Add(new Transaction
                        {
                            BalanceChange = 28,
                            Discription = "New Year",
                            EmployeeID = employee.EmployeeID,
                            Employee = employee,
                            TransactionDate = DateTime.Today,
                            TransactionID = Guid.NewGuid().ToString(),
                            TransactionType = "",
                        });*/
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
