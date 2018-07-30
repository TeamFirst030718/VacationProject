using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;
using VacationsBLL.Enums;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class VacationService : IVacationService
    {
        private IVacationRepository _vacationRepository;
        private IVacationStatusTypeRepository _vacationStatusTypeRepository;

        public VacationService(IVacationRepository vacations, IVacationStatusTypeRepository vacationStatusType)
        {
            _vacationRepository = vacations;
            _vacationStatusTypeRepository = vacationStatusType;
        }

        public void Create(VacationDTO vacation)
        {
            _vacationRepository.Add(Mapper.Map<VacationDTO, Vacation>(vacation));
        }

        public List<VacationDTO> GetVacations()
        {
            var vacations = _vacationRepository.Get();
            var vacationsDTO = new List<VacationDTO>();

            foreach (var vacation in vacations)
            {
                vacationsDTO.Add(Mapper.Map<Vacation, VacationDTO>(vacation));
            }

            return vacationsDTO;
        }

        public bool IsApproved(string id)
        {
            return _vacationStatusTypeRepository.GetById(id).VacationStatusName == VacationStatusTypeEnum.Approved.ToString();
        }
}
}
