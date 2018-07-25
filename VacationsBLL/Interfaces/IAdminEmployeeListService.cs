using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IAdminEmployeeListService
    {
        List<EmployeeListDTO> EmployeeList();
    }
}