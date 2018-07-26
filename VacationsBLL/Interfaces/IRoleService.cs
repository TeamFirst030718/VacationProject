using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        void Create(RoleDTO employee);
        List<RoleDTO> GetRoles();
    }
}
