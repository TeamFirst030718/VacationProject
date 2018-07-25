using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VacationsBLL.DTOs
{
    public class AspNetUserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<AspNetRoleDTO> AspNetRoles { get; set; }

    }
}
