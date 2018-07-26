using System;

namespace VacationsBLL.DTOs
{
    public class RequestProcessResultDTO
    {
        public string VacationID { get; set; }

        public int BalanceChange { get; set; }

        public string Discription { get; set; }

        public string EmployeeID { get; set; }

        public string Result { get; set; }

        public DateTime DateOfBegin { get; set; }

        public DateTime DateOfEnd { get; set; }
    }
}
