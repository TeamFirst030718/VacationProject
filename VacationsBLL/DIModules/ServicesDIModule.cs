using Ninject.Activation;
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
            Bind<IAdminRequestService>().To<AdminRequestService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IUserService>().To<UserService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IPageListsService>().To<PageListsService>();
            Bind<IProfileDataService>().To<ProfileDataService>();
            Bind<IRequestCreationService>().To<RequestCreationService>();
            Bind<IAdminEmployeeListService>().To<AdminEmployeeListService>();
        }   
    }
}
