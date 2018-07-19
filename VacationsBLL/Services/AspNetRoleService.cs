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

namespace VacationsBLL.Services
{
    public class AspNetRoleService: IAspNetRoleService
    {
        IAspNetRolesRepository _roles;

        public AspNetRoleService(IAspNetRolesRepository roles)
        {
            _roles = roles;
        }

        public void Create(AspNetRoleDTO aspNetRole)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AspNetRoleDTO, AspNetRole>()).CreateMapper();

            _roles.Add(mapper.Map<AspNetRoleDTO, AspNetRole>(aspNetRole));
        }

        public List<AspNetRoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AspNetRole, AspNetRoleDTO>()).CreateMapper();

            var aspNetRoles = _roles.GetAll();
            var aspNetRolesDTO = new List<AspNetRoleDTO>();

            foreach (var aspNetRole in aspNetRoles)
            {
                aspNetRolesDTO.Add(mapper.Map<AspNetRole, AspNetRoleDTO>(aspNetRole));
            }

            return aspNetRolesDTO;
        }

        public void SaveChanges()
        {
            _roles.Save();
        }

        public void Dispose()
        {
            _roles.Dispose();
        }
    }
}
