using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace Vacations.DIModules
{
    public class PageListsServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPageListsService>().To<PageListsService>();
        }
    }
}