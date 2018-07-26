using System.Collections.Generic;
using VacationsBLL.DTOs;
using VacationsBLL.Interfaces;
using VacationsDAL.Entities;
using VacationsDAL.Interfaces;

namespace VacationsBLL.Services
{
    public class RoleService: IRoleService
    {
        IRolesRepository _roles;

        public RoleService(IRolesRepository roles)
        {
            _roles = roles;
        }

        public void Create(RoleDTO aspNetRole)
        {


            _roles.Add(Mapper.Map<RoleDTO, AspNetRole>(aspNetRole));
        }

        public List<RoleDTO> GetRoles()
        {


            var roles = _roles.Get();
            var rolesDTO = new List<RoleDTO>();

            foreach (var role in roles)
            {
                rolesDTO.Add(Mapper.Map<AspNetRole, RoleDTO>(role));
            }

            return rolesDTO;
        }

        public void Dispose()
        {
            _roles.Dispose();
        }
    }
}
