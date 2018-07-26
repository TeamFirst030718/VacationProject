using System;

namespace VacationsBLL.DTOs
{
    public class ProfileVacationDTO
    {
        public DateTime DateOfBegin { get; set; }

        public DateTime DateOfEnd { get; set; }

        public string Comment { get; set; }

        public string VacationType { get; set; }

        public string VacationStatusType { get; set; }

        public int Duration { get; set; }
    }
}
