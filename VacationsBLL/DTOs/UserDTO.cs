using System.Collections.Generic;
namespace VacationsBLL.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<RoleDTO> AspNetRoles { get; set; }

    }
}
