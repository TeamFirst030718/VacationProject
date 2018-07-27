using System;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IRequestCreationService
    { 
        void CreateVacation(VacationDTO vacation);
        string GetStatusIdByType(string type);
    }
}