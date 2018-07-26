using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IProfileDataService
    {
        ProfileDTO GetUserData(string userEmail);
        VacationBalanceDTO GetUserVacationBalance(string userEmail);
        ProfileVacationDTO[] GetUserVacationsData(string userEmail);
    }
}