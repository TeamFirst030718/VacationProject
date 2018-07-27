using System;
using System.Collections.Generic;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IRoleService
    {
        void Create(RoleDTO employee);
        List<RoleDTO> GetRoles();
    }
}
