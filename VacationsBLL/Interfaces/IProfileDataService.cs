using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IProfileDataService : IDisposable
    {
        UserProfileDTO GetUserData(string userEmail);
        VacationBalanceDTO GetUserVacationBalance(string userEmail);
        List<ProfileVacationDTO> GetUserVacationsData(string userEmail);
        MapTo MapEntity<MapFrom, MapTo>(MapFrom entity);
        IEnumerable<EntityMapTo> MapCollection<EntityToMapFrom, EntityMapTo>(IEnumerable<EntityToMapFrom> entityCollection);
    }
}