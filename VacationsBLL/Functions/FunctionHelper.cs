using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Enums;

namespace VacationsBLL.Functions
{
    public static class FunctionHelper
    {
        public static int VacationSortFunc(string statusType)
        {
            if (statusType.Equals(VacationStatusTypeEnum.Pending.ToString()))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int EmployeeSortFunc(string teamName)
        {
            if (!teamName.Equals("Empty"))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
