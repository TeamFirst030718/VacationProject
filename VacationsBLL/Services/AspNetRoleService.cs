using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.Services
{
    public class AspNetRoleService: IAspNetRoleService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public AspNetRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(AspNetRoleDTO aspNetRole)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AspNetRoleDTO, AspNetRole>()).CreateMapper();

            _unitOfWork.AspNetRoles.Add(mapper.Map<AspNetRoleDTO, AspNetRole>(aspNetRole));
        }

        public List<AspNetRoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AspNetRole, AspNetRoleDTO>()).CreateMapper();

            var aspNetRoles = _unitOfWork.AspNetRoles.GetAll();
            var aspNetRolesDTO = new List<AspNetRoleDTO>();

            foreach (var aspNetRole in aspNetRoles)
            {
                aspNetRolesDTO.Add(mapper.Map<AspNetRole, AspNetRoleDTO>(aspNetRole));
            }

            return aspNetRolesDTO;
        }

        public void SaveChanges()
        {
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
