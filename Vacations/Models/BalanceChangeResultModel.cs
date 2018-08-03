using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class BalanceChangeResultModel
    {
        public string EmployeeID { get; set; }

        public string Comment { get; set; }

        public int Balance { get; set; }
    }
}