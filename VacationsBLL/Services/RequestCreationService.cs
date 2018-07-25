using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;
using VacationsDAL.Interfaces;
using VacationsBLL.DTOs;
using VacationsDAL.Entities;

namespace VacationsBLL.Services
{
    public class RequestCreationService : IRequestCreationService
    {
        private IVacationRepository _vacations;

        private IVacationStatusTypeRepository _vacationStatusTypes;

        private IMapService _mapService;

        public string GetStatusIdByType(string type)
        {
            return _vacationStatusTypes.GetByType(type).VacationStatusTypeID;
        }

        public RequestCreationService(IVacationRepository vacations, IMapService mapService, IVacationStatusTypeRepository vacationStatusTypes)
        {
            _vacations = vacations;
            _mapService = mapService;
            _vacationStatusTypes = vacationStatusTypes;
        }

        public void CreateVacation(VacationDTO vacation)
        {
            
            _vacations.Add(_mapService.Map<VacationDTO, Vacation>(vacation));
        }

        public void Dispose()
        {
            _vacations.Dispose();
            _vacationStatusTypes.Dispose();
        }
    }
}
