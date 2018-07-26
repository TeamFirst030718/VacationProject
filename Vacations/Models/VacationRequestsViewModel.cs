using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacationsBLL.DTOs;

namespace Vacations.Models
{
    public class VacationRequestsViewModel
    {
        public RequestDTO[] Requests { get; set; }
    }
}