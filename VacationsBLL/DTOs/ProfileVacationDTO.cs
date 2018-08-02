using System;

namespace VacationsBLL.DTOs
{
    public class ProfileVacationDTO
    {
        public DateTime DateOfBegin { get; set; }

        public DateTime DateOfEnd { get; set; }

        public string Comment { get; set; }

        public string VacationType { get; set; }

        public int Duration { get; set; }

        public string Status { get; set; }

        public DateTime Created { get; set; }
    }
}
