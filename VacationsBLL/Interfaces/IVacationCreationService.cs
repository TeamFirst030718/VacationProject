using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IVacationCreationService
    {
        void CreateVacation(VacationDTO vacation);
        string GetStatusIdByType(string type);
    }
}