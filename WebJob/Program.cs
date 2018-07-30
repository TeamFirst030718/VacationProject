using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using VacationsBLL;
using VacationsBLL.Interfaces;
using VacationsBLL.Services;
using VacationsDAL.Contexts;
using VacationsDAL.Interfaces;
using VacationsDAL.Repositories;

namespace WebJob
{
    class Program
    {

        static readonly Container container;

        static Program()
        {
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            container.Register<ITeamRepository, TeamRepository>(Lifestyle.Scoped);
            container.Register<ITransactionRepository, TransactionRepository>(Lifestyle.Scoped);
            container.Register<ITransactionTypeRepository, TransactionTypeRepository>(Lifestyle.Scoped);
            container.Register<IVacationRepository, VacationRepository>(Lifestyle.Scoped);
            container.Register<IVacationStatusTypeRepository, VacationStatusTypeRepository>(Lifestyle.Scoped);
            container.Register<IUsersRepository, UsersRepository>(Lifestyle.Scoped);
            container.Register<IRolesRepository, RolesRepository>(Lifestyle.Scoped);
            container.Register<IEmployeeRepository, EmployeeRepository>(Lifestyle.Scoped);
            container.Register<IJobTitleRepository, JobTitleRepository>(Lifestyle.Scoped);
            container.Register<IVacationTypeRepository, VacationTypeRepository>(Lifestyle.Scoped);
            container.Register<VacationsContext>(Lifestyle.Scoped);

            container.Register<IRequestService, RequestService>(Lifestyle.Scoped);
            container.Register<IRoleService, RoleService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IEmployeeService, EmployeeService>(Lifestyle.Scoped);
            container.Register<IPageListsService, PageListsService>(Lifestyle.Scoped);
            container.Register<IProfileDataService, ProfileDataService>(Lifestyle.Scoped);
            container.Register<ITeamService, TeamService>(Lifestyle.Scoped);
            container.Register<IRequestCreationService, RequestCreationService>(Lifestyle.Scoped);
            container.Register<IAdminEmployeeListService, AdminEmployeeListService>(Lifestyle.Scoped);
            container.Register<IPhotoUploadService, PhotoUploadService>(Lifestyle.Scoped);
            container.Register<IVacationService, VacationService>(Lifestyle.Scoped);
            container.Register<IEmailSendService, EmailSendService>(Lifestyle.Scoped);

            container.Verify();
        }

        static void Main(string[] args)
        {
            var temp = container.GetInstance<IVacationService>();
            var functions = new Functions(container.GetInstance<IVacationService>(), container.GetInstance<IEmailSendService>(), container.GetInstance<IEmployeeService>());
            functions.ProcessMessage();
        }
    }
}
