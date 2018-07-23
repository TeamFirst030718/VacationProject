using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Entities;

namespace VacationsBLL.DTOs
{
    class AspNetUserDTO
    {
        public virtual Employee Employee { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
