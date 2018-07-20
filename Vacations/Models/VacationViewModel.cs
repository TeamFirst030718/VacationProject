using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class VacationViewModel
    {
        public string VacationID { get; set; }


        public string EmployeeID { get; set; }


        public DateTime DateOfBegin { get; set; }


        public DateTime DateOfEnd { get; set; }


        public string Comment { get; set; }


        public string VacationStatusTypeID { get; set; }


        public string TransactionID { get; set; }
    }
}