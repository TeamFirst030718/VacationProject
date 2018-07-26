using System;

namespace VacationsBLL.DTOs
{
    public class TransactionDTO
    {
        public string TransactionID { get; set; }

        public int BalanceChange { get; set; }

        public string Discription { get; set; }

        public string EmployeeID { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionTypeID { get; set; }

    }
}
