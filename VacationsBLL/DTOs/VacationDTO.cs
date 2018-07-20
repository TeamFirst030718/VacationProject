using System;

namespace VacationsBLL.DTOs
{
    public class VacationDTO
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
