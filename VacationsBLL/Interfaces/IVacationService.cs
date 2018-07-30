using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IVacationService
    {
        void Create(VacationDTO employee);
        List<VacationDTO> GetVacations();
        bool IsApproved(string id);
    }
}
