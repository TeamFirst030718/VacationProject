using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vacations.Models
{
    public class UserViewModel
    {
        public ProfileViewModel ProfileData;
        public ProfileVacationsViewModel[] VacationsData;
        public VacationBalanceModel VacationBalanceData;
    }
}