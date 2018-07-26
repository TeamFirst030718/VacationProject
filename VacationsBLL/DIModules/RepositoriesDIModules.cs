using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsDAL.Contexts;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace VacationsBLL.DIModules
{
    public class RepositoriesDIModules : NinjectModule
    {
        public override void Load()
        {
            Bind<ITeamRepository>().To<TeamRepository>();
            Bind<ITransactionRepository>().To<TransactionRepository>();
            Bind<ITransactionTypeRepository>().To<TransactionTypeRepository>();
            Bind<IVacationRepository>().To<VacationRepository>();
            Bind<IVacationStatusTypeRepository>().To<VacationStatusTypeRepository>();
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<IRolesRepository>().To<RolesRepository>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IJobTitleRepository>().To<JobTitleRepository>();
            Bind<IVacationTypeRepository>().To<VacationTypeRepository>();
            Bind<VacationsContext>().ToSelf().InSingletonScope();
        }
    }
}
