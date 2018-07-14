using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacationsDAL.Interfaces;
using VacationsDAL.Unit;

namespace Vacations.DIModules
{
    public class EmployeeUnitModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeUnitOfWork>().To<EmployeeUnitOfWork>();
        }
    }
}