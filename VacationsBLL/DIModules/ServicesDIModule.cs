﻿using Ninject.Activation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;

namespace VacationsBLL.DIModules
{
    public class ServicesDIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAspNetRoleService>().To<AspNetRoleService>();
            Bind<IAspNetUserService>().To<AspNetUserService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IPageListsService>().To<PageListsService>();
            Bind<IProfileDataService>().To<ProfileDataService>();
            Bind<IVacationCreationService>().To<VacationCreationService>();
            Bind<IMapService>().To<MapService>().InSingletonScope(); 
        }   
    }
}