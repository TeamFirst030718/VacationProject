using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceResetWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var functions = new Functions();
            functions.UpdateBalance();
        }
    }
}
