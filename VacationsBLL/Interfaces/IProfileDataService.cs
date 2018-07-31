using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IProfileDataService
    {
        ProfileDTO GetUserData(string id);
        VacationBalanceDTO GetUserVacationBalance(string id);
        ProfileVacationDTO[] GetUserVacationsData(string id);
    }
}