using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IEmployeeListService
    {
        List<EmployeeListDTO> EmployeeList();
    }
}