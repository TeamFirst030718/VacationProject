using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationsBLL.Interfaces
{
    public interface IEmailSendService
    {
        Task SendAsync(string address, string name, string title, string plainTextContent, string message);
    }
}
