using System;

namespace VacationsBLL.DTOs
{
    public class ProfileDTO
    {
        public string WorkEmail { get; set; }
       
        public string Name { get; set; }

        public string Surname { get; set; }
  
        public DateTime BirthDate { get; set; }

        public string PersonalMail { get; set; }
    
        public string Skype { get; set; }
    
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; }
       
        public bool Status { get; set; }
       
        public DateTime? DateOfDismissal { get; set; }
     
        public int VacationBalance { get; set; }
    
        public string JobTitle { get; set; }
    
        public string TeamName { get; set; }
    
        public string TeamLeader{ get; set; }
    }
}
