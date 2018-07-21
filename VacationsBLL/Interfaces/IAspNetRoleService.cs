using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.DTOs;

namespace VacationsBLL.Interfaces
{
    public interface IAspNetRoleService : IDisposable
    {
        void Create(AspNetRoleDTO employee);
        List<AspNetRoleDTO> GetRoles();
    }
}
