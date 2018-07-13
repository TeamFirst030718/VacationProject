using Ninject.Modules;
using VacationsBLL;

namespace Vacations.DImodules
{
    public class EmployeeServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>();
        }
    }
}