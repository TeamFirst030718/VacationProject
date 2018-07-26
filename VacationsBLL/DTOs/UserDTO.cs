using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VacationsBLL.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<RoleDTO> AspNetRoles { get; set; }

    }
}
