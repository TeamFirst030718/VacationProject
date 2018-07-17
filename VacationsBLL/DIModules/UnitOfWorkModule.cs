using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacationsDAL.Interfaces;
using VacationsDAL.Unit;

namespace Vacations.DIModules
{
    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            /*.InSingletonScope();*/
        }
    }
}